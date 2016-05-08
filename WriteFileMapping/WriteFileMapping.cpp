// WriteFileMapping.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <stdlib.h>

int main(int argc, char* argv[])
{
	
	
	if (argc!=4)
	{
		printf("WriteFileMapping FileName Offset ByteValue\n");
		return 0;
	}


	HANDLE hFile=CreateFile(argv[1],GENERIC_READ|GENERIC_WRITE,FILE_SHARE_READ,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);
	if (hFile==INVALID_HANDLE_VALUE)
	{
		return 0;
	}
	
	HANDLE hMap=CreateFileMapping(hFile,NULL,PAGE_READWRITE,0,0,NULL);
	char* p=(char*)MapViewOfFile(hMap,FILE_MAP_READ|FILE_MAP_WRITE,0,0,0);
	
	int offset=atoi(argv[2]);
	int byteValue=atoi(argv[3]);
	p[offset]=(char)byteValue;

	return 0;
}

