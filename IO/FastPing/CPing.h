#pragma once

#include "StdAfx.h"
#include <windows.h>
#include <winsock2.h>

#include <stdio.h>
#pragma comment ( lib, "ws2_32.lib" )

///ICMP����ͷ
typedef struct icmp_hdr
{
    unsigned char  icmp_type;          // ����
    unsigned char  icmp_code;          // ��ֵ
    unsigned short icmp_checksum;      // У���

    unsigned short icmp_id;            // Id��
    unsigned short icmp_sequence;      // ����
    unsigned long  imcp_timestamp;     // ʱ���
} ICMP_HDR, *PICMP_HDR;


typedef struct ip_hdr
{
    unsigned char iphdr[20];		// 20���ֽڵ�ICMPͷ
} IP_HDR, *PIP_HDR;


#define PING_RECV_TIME          20   //���ּ�ʹ�����ӵ�����»��ǻ����������ICMP��
#define	ICMP_DATALEN	        32   // ICMP���ݳ���
#define RECVBUFLEN	        256    //���յĳ���


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

