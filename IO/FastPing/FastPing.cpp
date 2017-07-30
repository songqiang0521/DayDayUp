// FastPing.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "CPing.h"

//
//本工具并不是要严格地通过ICMPping通目标计算机
//而是要通过发包收包的方式来验证自己的网络是否已经通畅了
//所以，ping过程是做了特别处理的
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

