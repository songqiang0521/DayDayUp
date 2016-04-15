#include "stdafx.h"
#include "SharedMemoryQueueLib.h"

LPVOID SharedMemoryQueue :: getMsgPointer(unsigned index) const
{
	return ((char *)_MQBody + _MQBody -> shDataPos + index * _MQBody -> _messageSize);
}


SharedMemoryQueue::SharedMemoryQueue(char* queueName,unsigned int maxMessageCount,unsigned int messageSize,int* ret)
{
	char myObjectName[256];
	DWORD dwBufferSize=maxMessageCount*messageSize+sizeof(FixMQ);
	_hFileMap = CreateFileMapping(INVALID_HANDLE_VALUE, NULL,PAGE_READWRITE, 0, dwBufferSize, queueName);

	//共享区对象创建成功
	if (_hFileMap != NULL)
	{	
		_MQBody = (FixMQ*)MapViewOfFile(_hFileMap, FILE_MAP_ALL_ACCESS, 0, 0, 0);
		if (_MQBody != NULL)//映射成功
		{
			_MQBody -> shDataPos = sizeof(FixMQ);
			_MQBody -> shMaxNumber = maxMessageCount;
			_MQBody -> shCurrentNumber = 0;
			_MQBody ->_messageSize = messageSize;
			_MQBody -> shNextSlot = 0;
			_MQBody -> shNextUse = 0;
		}

		//创建信号灯
		strncpy(myObjectName, queueName, 254);
		strcpy(&myObjectName[strlen(queueName)], "S");
		_hSem = CreateSemaphore(NULL, 1, 1, myObjectName);
	
		//创建事件
		strcpy(&myObjectName[strlen(queueName)], "E");
		_hEvent = CreateEvent(NULL, TRUE, FALSE, myObjectName);
		*ret=0;
	}
	else
	{
		*ret =GetLastError();
	}
}

//获得已有的共享区上的定长FIFO信息队列对象
SharedMemoryQueue::SharedMemoryQueue(char* queueName,int* ret)
{
	char myObjectName[256];

	_hFileMap = OpenFileMapping(FILE_MAP_ALL_ACCESS, 0, queueName);
	if (_hFileMap == NULL)
	{
		*ret=GetLastError();
	}
	else
	{
		_MQBody = (FixMQ *)MapViewOfFile(_hFileMap, FILE_MAP_ALL_ACCESS, 0, 0, 0);

		//创建信号灯
		strncpy(myObjectName, queueName, 30);
		strcpy(&myObjectName[strlen(queueName)], "S");
		_hSem = OpenSemaphore(SEMAPHORE_MODIFY_STATE | SYNCHRONIZE, FALSE, myObjectName);
	
		//创建事件
		strcpy(&myObjectName[strlen(queueName)], "E");
		_hEvent = OpenEvent(EVENT_MODIFY_STATE | SYNCHRONIZE, FALSE, myObjectName);

		*ret=0;
	}
}


SharedMemoryQueue :: ~SharedMemoryQueue()
{
	if(_MQBody!=NULL)
	{
	   UnmapViewOfFile(_MQBody);
	}
	CloseHandle(_hFileMap);
	CloseHandle(_hSem);
	CloseHandle(_hEvent);
}

//赋值
SharedMemoryQueue & SharedMemoryQueue :: operator = (const SharedMemoryQueue & right)
{	
	_MQBody = right._MQBody;
	_hFileMap = right._hFileMap;
	_hSem = right._hSem;
	_hEvent = right._hEvent;
	return *this;
}


//清除队列中所有元素
void SharedMemoryQueue :: deleteAllMsgs()
{
	WaitForSingleObject(_hSem, INFINITE);
	_MQBody -> shCurrentNumber = 0;
	_MQBody -> shNextSlot = 0;
 	_MQBody -> shNextUse = 0;
	ResetEvent(_hEvent);
	ReleaseSemaphore(_hSem, 1, NULL);
}

bool SharedMemoryQueue :: IsEmpty() const
{
	//如果队列空返回true
	//如果shNextSlot和shNextUse指向相同位置, 则表明队列空
	return _MQBody -> shNextSlot == _MQBody -> shNextUse;
}

int SharedMemoryQueue :: GetCurrentMessageCount() const
{
	short count;
	DWORD ret1;
	BOOL ret2;
	ret1 = WaitForSingleObject(_hSem, INFINITE);
	count =  _MQBody -> shCurrentNumber;
	ret2 = ReleaseSemaphore(_hSem, 1, NULL);
	return count;
}

int SharedMemoryQueue :: SendMessage(LPVOID buffer,int bufferLength)
{
	WaitForSingleObject(_hSem, INFINITE);
	if (_MQBody -> shCurrentNumber == _MQBody -> shMaxNumber)//队列满！
	{
		ReleaseSemaphore(_hSem, 1, NULL);
		return QUEUE_OVERFLOW;
	}

	if(bufferLength>_MQBody ->_messageSize)
	{
		return -1;
	}
	//将新元素加在队尾, 并将标志指向下一个空位
	LPVOID p=getMsgPointer(_MQBody -> shNextSlot);
	memcpy(p,buffer, bufferLength);
	_MQBody -> shNextSlot++;
	if (_MQBody -> shNextSlot >= _MQBody -> shMaxNumber)
	{
		_MQBody -> shNextSlot = 0;
	}
	_MQBody -> shCurrentNumber ++;
	//设置队列有信息
	SetEvent(_hEvent);
	ReleaseSemaphore(_hSem, 1, NULL);
	return 0;
}

int SharedMemoryQueue :: RecieveMsg(LPVOID buffer, int bufferLength,DWORD dwTimeout)
{
	if(bufferLength<_MQBody->_messageSize)
	{
		return -1;
	}
	//等待队列为有信号状态
	DWORD ret1;
	BOOL ret2;
	HANDLE hEv[1];
	hEv[0] = _hEvent;
	ret1 = WaitForMultipleObjects(1, hEv, FALSE, dwTimeout);
	
	if (ret1 == WAIT_TIMEOUT) 
		return ret1;
	if (ret1 == WAIT_OBJECT_0 + 1)
		return ret1;

	ret1 = WaitForSingleObject(_hSem, INFINITE);
	
	if (_MQBody -> shCurrentNumber <= 0)
	{
		ret2 = ResetEvent(_hEvent);
		
		ret2 = ReleaseSemaphore(_hSem, 1, NULL);
		
		return UNEXPACT_EMPTY;
	}
	//删除队首元素
	int shDataPosloc = _MQBody -> shNextUse;
	_MQBody -> shNextUse++;
	if (_MQBody -> shNextUse >= _MQBody -> shMaxNumber)
		_MQBody -> shNextUse = 0;
	memcpy(buffer, getMsgPointer(shDataPosloc), _MQBody ->_messageSize);
	_MQBody -> shCurrentNumber --;
	if (_MQBody -> shCurrentNumber <= 0)
	{
		ret2 = ResetEvent(_hEvent);
		
	}
	ret2 = ReleaseSemaphore(_hSem, 1, NULL);

	return 0;
}

int SharedMemoryQueue :: CopyMsg(LPVOID buffer,int bufferLength, DWORD dwTimeout)
{
	if(bufferLength<_MQBody->_messageSize)
	{
		return -1;
	}
	//等待队列为有信号状态
	DWORD ret1;
	BOOL ret2;
	HANDLE hEv[1];
	hEv[0] = _hEvent;
	ret1 = WaitForMultipleObjects(1, hEv, FALSE, dwTimeout);
	
	if (ret1 == WAIT_TIMEOUT) 
		return ret1;

	ret1 = WaitForSingleObject(_hSem, INFINITE);
	
	if (_MQBody -> shCurrentNumber <= 0)
	{
		ret2 = ResetEvent(_hEvent);
		
		ret2 = ReleaseSemaphore(_hSem, 1, NULL);
		
		return UNEXPACT_EMPTY;
	}
	//拷贝队首元素
	memcpy(buffer, getMsgPointer(_MQBody -> shNextUse), _MQBody ->_messageSize);
	ret2 = ReleaseSemaphore(_hSem, 1, NULL);

	return 0;
}

























