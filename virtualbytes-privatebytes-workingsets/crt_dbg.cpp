//
//
//https://social.msdn.microsoft.com/Forums/vstudio/en-US/307d658a-f677-40f2-bdef-e6352b0bfe9e/malloc-fails-due-to-virtual-size-never-freed-any-more-under-windows-xp-and-visual-studio-2005?forum=vcgeneral
//
//
// crt_dbg.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int main(int argc, char* argv[])
{
	printf("Hello World!\n");

	for(int index=0;index<200;index++)
	{
		int** p;
		
		//当每次申请的内存小于512K时，调用delete后虚拟内存的数值不会降低，私有内存和工作集会降低
		//当每次申请的内存大于512K时，调用delete后虚拟内存也会降低
		int n=1*1024*512/4;
		
		p=new int*[n];
		
		for (int i=0;i<100;i++)
		{
			p[i]=new int[n];
		}
		
		////////////////////////////////
		for (int j=0;j<100;j++)
		{
			delete[] p[j];
		}
		
		delete[] p;
		
	}
	return 0;
}

