// ConsoleApplication1.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <windows.h>
struct MyStruct
{
    HANDLE file;
    DWORD key;
    char* buffer;
    int bufferLength;

};
HANDLE g_iocp;
MyStruct g_files[2];

DWORD WINAPI ThreadProc(LPVOID param)
{
    DWORD ret = 0;
    HANDLE iocp = (HANDLE)param;
    DWORD bytes = 0;
    DWORD key = 0;
    LPOVERLAPPED o;
    BOOL wait = FALSE;
    while (true)
    {
        wait = GetQueuedCompletionStatus(iocp, &bytes, &key, &o, INFINITE);
        DWORD le = GetLastError();
        if (wait == TRUE)
        {
            printf("bytes=%d", bytes);
            printf("key=%d", key);

            char buffer[10] = { 0 };
            memcpy(buffer, g_files[key].buffer + 1024 * 1024, 10);
            printf(buffer);
        }
    }

    return ret;
}


int main()
{

    //file1和file2的大小都是1G多
    HANDLE hFile1 = CreateFile(L"X:\\file1.txt", GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
    DWORD le = GetLastError();

    HANDLE hFile2 = CreateFile(L"X:\\file2.txt", GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_ALWAYS, FILE_FLAG_OVERLAPPED, NULL);
    le = GetLastError();
    g_files[0].key = 0;
    g_files[0].file = hFile1;
    g_files[1].key = 1;
    g_files[1].file = hFile2;

    g_iocp = CreateIoCompletionPort(INVALID_HANDLE_VALUE, NULL, 0, 4);

    CreateIoCompletionPort(g_files[0].file, g_iocp, g_files[0].key, 0);
    CreateIoCompletionPort(g_files[1].file, g_iocp, g_files[1].key, 0);

    for (size_t i = 0; i < 2; i++)
    {
        DWORD tid = 0;
        CreateThread(NULL, 0, ThreadProc, g_iocp, 0, &tid);
    }

    Sleep(2000);

    g_files[0].buffer = new char[1024 * 1024 * 200];
    memset(g_files[0].buffer, 0, 1024 * 1024 * 200);
    OVERLAPPED o = { 0 };
    DWORD read = 0;
    ReadFile(g_files[0].file, g_files[0].buffer, 1024 * 1024 * 200, &read, &o);

    //Sleep(2000*10);

    g_files[1].buffer = new char[1024 * 1024 * 200];
    memset(g_files[1].buffer, 0, 1024 * 1024 * 200);
    ReadFile(g_files[1].file, g_files[1].buffer, 1024 * 1024 * 200, &read, &o);

    SuspendThread(GetCurrentThread());

    return 0;
}

