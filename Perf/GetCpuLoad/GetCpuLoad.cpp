// GetCpuLoad.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>
#include <Pdh.h>
#include <PdhMsg.h>

#pragma comment(lib,"pdh.lib")

int GetIndex(char* strIndex)
{
    if (_stricmp(strIndex, "_Total") == 0)
    {
        return -1;
    }
    else
    {
        return atoi(strIndex);
    }
}


void UpdateCPUValues(double* cpuValues, DWORD cpuCount, HQUERY hQuery, HCOUNTER hCounter, DWORD dwBufferSize, PDH_FMT_COUNTERVALUE_ITEM* pItems)
{
    PDH_STATUS status;
    DWORD dwItemCount = 0;

    status = PdhCollectQueryData(hQuery);
    if (ERROR_SUCCESS == status)
    {
        status = PdhGetFormattedCounterArray(hCounter, PDH_FMT_DOUBLE, &dwBufferSize, &dwItemCount, pItems);
        if (ERROR_SUCCESS == status)
        {
            // Loop through the array and print the instance name and counter value.
            for (DWORD i = 0; i < dwItemCount; i++)
            {
                int index = GetIndex(pItems[i].szName);
                if (index == -1)
                {
                    printf("total=%.2f\n", pItems[i].FmtValue.doubleValue);
                }
                else if (index >= 0 && index<cpuCount)
                {
                    cpuValues[i] = pItems[i].FmtValue.doubleValue;
                    printf("cpu:%d  =%.2f\n", index, pItems[i].FmtValue.doubleValue);
                }
                else
                {
                    continue;
                }
            }
            printf("\n\n\n\n");
        }
    }
    else
    {
        printf("error-error-error-error-error-error-error-error-error-error-error\n\n\n");
    }
}

int main()
{
    DWORD  cpuCount = 0;
    SYSTEM_INFO siSysInfo;
    GetSystemInfo(&siSysInfo);
    cpuCount = siSysInfo.dwNumberOfProcessors;

    HQUERY hQuery = NULL;
    HCOUNTER hCounter = NULL;
    PDH_STATUS status = ERROR_SUCCESS;
    DWORD dwFormat = PDH_FMT_DOUBLE;
    DWORD dwBufferSize = 0;         // Size of the pItems buffer
    DWORD dwItemCount = 0;          // Number of items in the pItems buffer
    PDH_FMT_COUNTERVALUE_ITEM *pItems = NULL;  // Array of PDH_FMT_COUNTERVALUE_ITEM structures

    status = PdhOpenQuery(NULL, 0, &hQuery);
    if (ERROR_SUCCESS != status)
    {
        printf("PdhOpenQuery failed with 0x%x\n", status);
    }

    char* counter = "\\Processor(*)\\% Processor Time";
    status = PdhAddCounter(hQuery, counter, 0, &hCounter);

    if (ERROR_SUCCESS != status)
    {
        printf("PdhAddCounter failed with 0x%x\n", status);
    }

    status = PdhCollectQueryData(hQuery);
    if (ERROR_SUCCESS != status)
    {
        printf("PdhCollectQueryData failed with 0x%x\n", status);
    }

    //得到需要分配的缓冲区大小
    status = PdhGetFormattedCounterArray(hCounter, PDH_FMT_DOUBLE, &dwBufferSize, &dwItemCount, pItems);
    if (PDH_MORE_DATA == status)
    {
        pItems = (PDH_FMT_COUNTERVALUE_ITEM *) new char[dwBufferSize];
    }
    else
    {
        dwBufferSize = 1024;
        pItems = (PDH_FMT_COUNTERVALUE_ITEM *) new char[dwBufferSize];
    }

    double cpuValues[64] = { 0.0 };
    while (1)
    {
        UpdateCPUValues(cpuValues, cpuCount, hQuery, hCounter, dwBufferSize, pItems);
        Sleep(1000);
    }

    return 0;
}

