//--------------------------------------------------------------------//
//建立在共享区上带诊断事件的FIFO消息队列的声明:CASmsgq.h
//--------------------------------------------------------------------//

#ifndef SHARED_MEMORY_QUEUE
#define SHARED_MEMORY_QUEUE
#include "stdafx.h"

const int DIAG_REQ = 1;
const int UNEXPACT_EMPTY = -1;	//例外，消息队列空
const int QUEUE_OVERFLOW = -2;	//队列满
const int MSGQ_CREATEFAIL = -3;	//消息队列创建失败


struct FixMQ
{
	unsigned int shDataPos;			//消息区首相对于共享区首址的偏移
	unsigned int shMaxNumber;			//队列最大消息数
	unsigned int shCurrentNumber;		//当前队列中存在的消息数
	unsigned int _messageSize;		//消息长度
	unsigned int shNextSlot;			//下次要入队的位置
	unsigned int shNextUse; 			//下次要出队的位置
};


class SharedMemoryQueue
{
private:
	LPVOID getMsgPointer(unsigned index) const;		//得到信息数组指定下标元素的地址
public:
	//构造函数
	//SharedMemoryQueue();
	SharedMemoryQueue(char* queueName,unsigned int maxMessageCount,unsigned int messageSize,int* ret);
	SharedMemoryQueue(char* queueName,int* ret);

	virtual ~SharedMemoryQueue();	//析构函数

	SharedMemoryQueue & operator = (const SharedMemoryQueue &);	//赋值

	//队列操作
	int GetCurrentMessageCount() const;			//得到队列中当前消息个数
	void deleteAllMsgs();				//清除队列中全部消息
	bool IsEmpty() const;				//判断队列是否为空

	int RecieveMsg(LPVOID recieveBuffer,//返回的消息指针
		int bufferLewngth,
		DWORD dwTimeout					//阻塞超时
		);	//阻塞等待队列发信号。当队列不空时，从队列首取走一条消息

	int CopyMsg(LPVOID recieveBuffer,	//返回的消息指针
		int bufferLewngth,
		DWORD dwTimeout					//阻塞超时
		);	//阻塞等待队列发信号。当队列不空时，从队列首拷贝一条消息，拷走的消息还在队列中
	
	int SendMessage(LPVOID buffer,int bufferLength);		//向队列追加一条消息
private:
	//数据域
	FixMQ * _MQBody;			//放在共享文件映象上的消息队列体
	HANDLE _hFileMap;			//共享文件映象句柄
	HANDLE _hSem;				//信号灯句柄，用于访问互斥
	HANDLE _hEvent;				//事件句柄，用于访问同步
};


#endif