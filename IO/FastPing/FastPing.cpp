// FastPing.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "CPing.h"

//
//�����߲�����Ҫ�ϸ��ͨ��ICMPpingͨĿ������
//����Ҫͨ�������հ��ķ�ʽ����֤�Լ��������Ƿ��Ѿ�ͨ����
//���ԣ�ping�����������ر����
//


int main()
{
    CPing* pinger = new CPing(50);
    BOOL pingState = FALSE;
    while (true)
    {
        pingState=pinger->Ping("123.123.123.123", "hello");
    
        if (pingState==TRUE)
        {
            printf("OK\n");
        }
        else
        {
            printf("FAIL\n");
        }

        Sleep(100);
    }


    return 0;
}

