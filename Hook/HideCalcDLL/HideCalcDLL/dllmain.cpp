// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <Windows.h>
#include <winternl.h>
#include "MinHook.h"

#pragma comment(lib,"Minhook.x86.lib")

#define STATUS_SUCCESS  ((NTSTATUS)0x00000000L)


//
//no calc
//
typedef BOOL(WINAPI *CREATEPROCESSW)(
    _In_opt_    LPCTSTR               lpApplicationName,
    _Inout_opt_ LPTSTR                lpCommandLine,
    _In_opt_    LPSECURITY_ATTRIBUTES lpProcessAttributes,
    _In_opt_    LPSECURITY_ATTRIBUTES lpThreadAttributes,
    _In_        BOOL                  bInheritHandles,
    _In_        DWORD                 dwCreationFlags,
    _In_opt_    LPVOID                lpEnvironment,
    _In_opt_    LPCTSTR               lpCurrentDirectory,
    _In_        LPSTARTUPINFO         lpStartupInfo,
    _Out_       LPPROCESS_INFORMATION lpProcessInformation
    );
CREATEPROCESSW fpCreateProcessW_org = NULL;

BOOL(WINAPI CreateProcessW_detour)(
    _In_opt_    LPCTSTR               lpApplicationName,
    _Inout_opt_ LPTSTR                lpCommandLine,
    _In_opt_    LPSECURITY_ATTRIBUTES lpProcessAttributes,
    _In_opt_    LPSECURITY_ATTRIBUTES lpThreadAttributes,
    _In_        BOOL                  bInheritHandles,
    _In_        DWORD                 dwCreationFlags,
    _In_opt_    LPVOID                lpEnvironment,
    _In_opt_    LPCTSTR               lpCurrentDirectory,
    _In_        LPSTARTUPINFO         lpStartupInfo,
    _Out_       LPPROCESS_INFORMATION lpProcessInformation
    )
{
    const wchar_t* p = NULL;
    if (lpApplicationName != NULL)
    {
        p = wcsstr(lpApplicationName, L"calc");
        if (p != NULL)
        {
            MessageBox(NULL,lpApplicationName,lpApplicationName,MB_OK);
            return ERROR_FILE_NOT_FOUND;
        }
    }
    if (lpCommandLine != NULL)
    {
        p = wcsstr(lpCommandLine, L"calc");
        if (p != NULL)
        {
            MessageBox(NULL, lpCommandLine, lpCommandLine, MB_OK);
            return ERROR_FILE_NOT_FOUND;
        }
    }

    return fpCreateProcessW_org(
        lpApplicationName,
        lpCommandLine,
        lpProcessAttributes,
        lpThreadAttributes,
        bInheritHandles,
        dwCreationFlags,
        lpEnvironment,
        lpCurrentDirectory,
        lpStartupInfo,
        lpProcessInformation
    );

}


BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    {
        MH_Initialize();

        MH_STATUS state = MH_CreateHook(&CreateProcessW, CreateProcessW_detour, (LPVOID*)(&fpCreateProcessW_org));
        state = MH_EnableHook(&CreateProcessW);

        break;
    }


    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

