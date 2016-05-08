// SharedMemoryQueue.cpp : 定义控制台应用程序的入口点。
//


#include "SharedMemoryQueueLib.h"




int _tmain(int argc, _TCHAR* argv[])
{

	int ret=0;
	SharedMemoryQueue* queue=new SharedMemoryQueue("queue_sq",1024,128,&ret);

	char buffer[128]="hello,from queue";
	char buffer2[128]={0};
	queue->SendMessage(buffer,128);

	int xx=0;

	while(xx<1000*10)
	{
		xx++;
		for(int i=0;i<1000;i++)
		{
			queue->SendMessage(buffer,128);
		}

		for(int i=0;i<1000;i++)
		{
			queue->RecieveMsg(buffer2,128,10);
		}
	}
	

	queue->SendMessage(buffer,128);
	queue->SendMessage(buffer,128);
	queue->SendMessage(buffer,128);
	queue->SendMessage(buffer,128);











	getchar();
	return 0;
}

