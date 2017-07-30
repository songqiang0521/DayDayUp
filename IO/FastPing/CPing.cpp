
#include "StdAfx.h"
#include "CPing.h"





void CPing::ConfigPing()
{
    m_recvBufferLen = RECVBUFLEN;
    m_sendBufferLen = sizeof(ICMP_HDR) + ICMP_DATALEN;
    m_recvBuffer = new char[m_recvBufferLen];
    m_sendBuffer = new char[m_sendBufferLen];
    m_threadID = GetCurrentThreadId();

    WSADATA wsa = { 0 };
    WSAStartup(MAKEWORD(2, 2), &wsa);
    m_pingSock = socket(AF_INET, SOCK_RAW, IPPROTO_ICMP);
    if (m_pingSock == SOCKET_ERROR)
    {
        printf("Create socket error\n");
        //WSACleanup();
        return;
    }

    int setok;
    setok = setsockopt(m_pingSock, SOL_SOCKET, SO_SNDTIMEO, (const char *)&m_sendTimeOut, sizeof(m_sendTimeOut));
    setok = setsockopt(m_pingSock, SOL_SOCKET, SO_RCVTIMEO, (const char *)&m_recvTimeOut, sizeof(m_recvTimeOut));


    int bufLen = 80;
    setok = setsockopt(m_pingSock, SOL_SOCKET, SO_SNDBUF, (char*)&bufLen, sizeof(bufLen));
    setok = setsockopt(m_pingSock, SOL_SOCKET, SO_RCVBUF, (char*)&bufLen, sizeof(bufLen));


    FD_ZERO(&m_readfds);
    FD_SET(m_pingSock, &m_readfds);
    m_timeout.tv_sec = 0;
    m_timeout.tv_usec = m_timeOut * 1000;//��λ��΢��

}

unsigned short CPing::GetCheckSum(unsigned short * buf, int len)
{
    unsigned long sum = 0;
    while (len > 1)
    {
        sum += *buf;
        ++buf;
        len -= sizeof(unsigned short);
    }
    if (len)
    {
        sum += *(UCHAR*)buf;
    }
    sum = (sum >> 16) + (sum & 0xffff);
    sum += (sum >> 16);
    return (USHORT)(~sum);
}



BOOL CPing::Ping(char* ipAddress, char* msg)
{
    int netRet = 0;
    PICMP_HDR pIcmphdr = NULL;
    struct sockaddr_in pingAddr = { 0 };

    pingAddr.sin_family = AF_INET;
    pingAddr.sin_addr.s_addr = inet_addr(ipAddress);
    pingAddr.sin_port = htons(0);

    memset(m_sendBuffer, 0x0, m_sendBufferLen);

    pIcmphdr = (PICMP_HDR)m_sendBuffer;
    pIcmphdr->icmp_type = 8;
    pIcmphdr->icmp_code = 0;
    pIcmphdr->icmp_id = (unsigned short)m_threadID;
    ++m_seq;
    pIcmphdr->icmp_sequence = m_seq;

    pIcmphdr->imcp_timestamp = GetTickCount();
    pIcmphdr->icmp_checksum = 0;
    memset(m_sendBuffer + sizeof(ICMP_HDR), 'S', ICMP_DATALEN);

    int msgLen = strlen(msg);
    if (msgLen <= ICMP_DATALEN)
    {
        memcpy(m_sendBuffer + sizeof(ICMP_HDR), msg, msgLen);
    }

    pIcmphdr->icmp_checksum = GetCheckSum((unsigned short *)m_sendBuffer, m_sendBufferLen);

    netRet = sendto(m_pingSock, m_sendBuffer, m_sendBufferLen, 0, (struct sockaddr *)&pingAddr, sizeof(pingAddr));
    if (netRet<0)
    {
        int xxx = WSAGetLastError();
    }


    BOOL pingState = FALSE;

    int fromLen = sizeof(pingAddr);
    for (DWORD i = 0; i<m_tryTimes; i++)
    {
        FD_ZERO(&m_readfds);
        FD_SET(m_pingSock, &m_readfds);
        m_timeout.tv_sec = 0;
        m_timeout.tv_usec = m_timeOut * 1000;//΢��

        int selectResult = 0;
        selectResult = select(0, &m_readfds, NULL, NULL, &m_timeout);
        if (selectResult <= 0)
        {
            continue;
        }

        memset(m_recvBuffer, 0x0, m_recvBufferLen);
        netRet = recvfrom(m_pingSock, m_recvBuffer, m_recvBufferLen, 0, (struct sockaddr *)&pingAddr, &fromLen);

        if (netRet == (sizeof(IP_HDR) + sizeof(ICMP_HDR) + ICMP_DATALEN))
        {
            pingState = TRUE;
            break;
        }

        //
        //ֻҪ���յ����ݰ��Ϳ�����Ϊ�Է�ʵ�����Ǵ��ڵ�
        //����˵���Լ�������ʵ�����Ǻõ�
        //���طǵõȵ��Լ��յ��Լ�����ȥ��icmp��
        //
        //if(netRet<0)
        //{
        //	continue;
        //}
        //else
        //{
        //	pIcmphdr = (PICMP_HDR)(m_recvBuffer+sizeof(IP_HDR));
        //	if(pIcmphdr->icmp_type != 0)
        //	{
        //		continue;
        //	}
        //	
        //	if(pIcmphdr->icmp_id != (unsigned short)m_threadID)
        //	{
        //		continue;
        //	}
        //	
        //	pingState= TRUE;//pingͨ��
        //	break;
        //}
    }

    return pingState;
}

CPing::~CPing()
{

}

CPing::CPing(DWORD timeOut)
{
    m_timeOut = timeOut;
    m_sendTimeOut = timeOut;
    m_recvTimeOut = timeOut;

    //����һ���ǿ��������������Ƿ�������Ҫ���
    //��Ϊ��SO_SNDBUF��SO_RCVBUF�ĳ��ȶ���������100��
    m_tryTimes = 1;

    m_seq = 0;

    ConfigPing();
}
