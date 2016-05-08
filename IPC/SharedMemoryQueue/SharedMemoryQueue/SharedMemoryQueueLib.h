//--------------------------------------------------------------------//
//�����ڹ������ϴ�����¼���FIFO��Ϣ���е�����:CASmsgq.h
//--------------------------------------------------------------------//

#ifndef SHARED_MEMORY_QUEUE
#define SHARED_MEMORY_QUEUE
#include "stdafx.h"

const int DIAG_REQ = 1;
const int UNEXPACT_EMPTY = -1;	//���⣬��Ϣ���п�
const int QUEUE_OVERFLOW = -2;	//������
const int MSGQ_CREATEFAIL = -3;	//��Ϣ���д���ʧ��


struct FixMQ
{
	unsigned int shDataPos;			//��Ϣ��������ڹ�������ַ��ƫ��
	unsigned int shMaxNumber;			//���������Ϣ��
	unsigned int shCurrentNumber;		//��ǰ�����д��ڵ���Ϣ��
	unsigned int _messageSize;		//��Ϣ����
	unsigned int shNextSlot;			//�´�Ҫ��ӵ�λ��
	unsigned int shNextUse; 			//�´�Ҫ���ӵ�λ��
};


class SharedMemoryQueue
{
private:
	LPVOID getMsgPointer(unsigned index) const;		//�õ���Ϣ����ָ���±�Ԫ�صĵ�ַ
public:
	//���캯��
	//SharedMemoryQueue();
	SharedMemoryQueue(char* queueName,unsigned int maxMessageCount,unsigned int messageSize,int* ret);
	SharedMemoryQueue(char* queueName,int* ret);

	virtual ~SharedMemoryQueue();	//��������

	SharedMemoryQueue & operator = (const SharedMemoryQueue &);	//��ֵ

	//���в���
	int GetCurrentMessageCount() const;			//�õ������е�ǰ��Ϣ����
	void deleteAllMsgs();				//���������ȫ����Ϣ
	bool IsEmpty() const;				//�ж϶����Ƿ�Ϊ��

	int RecieveMsg(LPVOID recieveBuffer,//���ص���Ϣָ��
		int bufferLewngth,
		DWORD dwTimeout					//������ʱ
		);	//�����ȴ����з��źš������в���ʱ���Ӷ�����ȡ��һ����Ϣ

	int CopyMsg(LPVOID recieveBuffer,	//���ص���Ϣָ��
		int bufferLewngth,
		DWORD dwTimeout					//������ʱ
		);	//�����ȴ����з��źš������в���ʱ���Ӷ����׿���һ����Ϣ�����ߵ���Ϣ���ڶ�����
	
	int SendMessage(LPVOID buffer,int bufferLength);		//�����׷��һ����Ϣ
private:
	//������
	FixMQ * _MQBody;			//���ڹ����ļ�ӳ���ϵ���Ϣ������
	HANDLE _hFileMap;			//�����ļ�ӳ����
	HANDLE _hSem;				//�źŵƾ�������ڷ��ʻ���
	HANDLE _hEvent;				//�¼���������ڷ���ͬ��
};


#endif