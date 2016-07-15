// Performance.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Allocs.h"
// Include VLD so that we can detect memory leaks.
#include "../trunk/vld.h"
//#include <windows.h>

int _tmain(int argc, _TCHAR* argv[])
{
	ULONGLONG startTime = GetTickCount64();
	// I am interested in how long it takes to collect and track allocations.
	// Not in how long how to dump the leak report at the end, nor rather if there are even
	// memory leaks at all. Hence do_free is false
	bool do_free = true;
	for (int i = 0; i < 150000; i++)
	{
		for (int type = 0; type < eCount; type++)
		{
			Alloc((LeakOption)type, do_free);
		}
	}
	ULONGLONG endTime = GetTickCount64();
	ULONGLONG total_execution = endTime - startTime;
	_tprintf(_T("Total Execution Time: %u (milliseconds)\n"), total_execution);
	
	return 0;
}

