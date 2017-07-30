#pragma once

#include "StdAfx.h"
#include <windows.h>
#include <winsock2.h>

#include <stdio.h>
#pragma comment ( lib, "ws2_32.lib" )

///ICMP报文头
typedef struct icmp_hdr
{
    unsigned char  icmp_type;          // 类型
    unsigned char  icmp_code;          // 码值
    unsigned short icmp_checksum;      // 校验和

    unsigned short icmp_id;            // Id号
    unsigned short icmp_sequence;      // 序列
    unsigned long  imcp_timestamp;     // 时间戳
} ICMP_HDR, *PICMP_HDR;


typedef struct ip_hdr
{
    unsigned char iphdr[20];		// 20个字节的ICMP头
} IP_HDR, *PIP_HDR;


#define PING_RECV_TIME          20   //发现即使在连接的情况下还是会接受其它的ICMP包
#define	ICMP_DATALEN	        32   // ICMP数据长度
#define RECVBUFLEN	        256    //接收的长度


class CPing
{

private:
    void ConfigPing();
    unsigned short GetCheckSum(unsigned short * buf, int len);

    SOCKET m_pingSock;
    DWORD m_threadID;

    DWORD m_tryTimes;

    DWORD m_timeOut;
    DWORD m_sendTimeOut;
    DWORD m_recvTimeOut;

    char* m_sendBuffer;
    int m_sendBufferLen;

    char* m_recvBuffer;
    int m_recvBufferLen;
    int m_seq;

    fd_set m_readfds;
    timeval m_timeout;

public:
    CPing(DWORD timeOut);
    ~CPing();
    BOOL Ping(char* ipAddress, char* msg);
};

