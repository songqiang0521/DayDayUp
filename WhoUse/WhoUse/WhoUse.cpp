// Written by Zoltan Csizmadia, zoltan_csizmadia@yahoo.com
// For companies(Austin,TX): If you would like to get my resume, send an email.
//
// The source is free, but if you want to use it, mention my name and e-mail address
//
//////////////////////////////////////////////////////////////////////////////////////
//
// WhoUses.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <tchar.h>
#include "SystemInfo.h"

LPCTSTR GetFileNamePosition(LPCTSTR lpPath)
{
    LPCTSTR lpAct = lpPath + _tcslen(lpPath);

    while (lpAct > lpPath && *lpAct != _T('\\') && *lpAct != _T('/'))
        lpAct--;

    if (lpAct > lpPath)
        lpAct++;

    return lpAct;
}

void WhoUsesModule(LPCTSTR lpFileName, BOOL bFullPathCheck)
{
    CString processName;
    BOOL bShow = FALSE;
    SystemProcessInformation::SYSTEM_PROCESS_INFORMATION* p;

    SystemProcessInformation pi;
    SystemModuleInformation mi;

    if (!mi.Refresh())
    {
        _tprintf(_T("SystemModulesInformation::Refresh() failed.\n"));
        return;
    }

    if (mi.m_ModuleInfos.GetHeadPosition() == NULL)
    {
        _tprintf(_T("No module information\n"));
        return;
    }

    pi.Refresh();

    _tprintf(_T("%-6s  %-20s  %s\n"), _T("PID"), _T("Name"), _T("Path"));
    _tprintf(_T("------------------------------------------------------------------\n"));

    for (POSITION pos = mi.m_ModuleInfos.GetHeadPosition(); pos != NULL; )
    {
        SystemModuleInformation::MODULE_INFO& m = mi.m_ModuleInfos.GetNext(pos);

        if (bFullPathCheck)
            bShow = _tcsicmp(m.FullPath, lpFileName) == 0;
        else
            bShow = _tcsicmp(GetFileNamePosition(m.FullPath), lpFileName) == 0;

        if (bShow)
        {
            if (pi.m_ProcessInfos.Lookup(m.ProcessId, p))
            {
                SystemInfoUtils::Unicode2CString(&p->usName, processName);
            }
            else
                processName = "";

            _tprintf(_T("0x%04X  %-20s  %s\n"),
                m.ProcessId,
                processName,
                m.FullPath);
        }
    }
}

DWORD CloseRemoteHandle(DWORD processID, HANDLE handle)
{
    HANDLE ht = 0;
    DWORD rc = 0;

    _tprintf(_T("Closing handle in process #%d ... "),
        processID);

    // open the process
    HANDLE hProcess = OpenProcess(PROCESS_CREATE_THREAD
        | PROCESS_VM_OPERATION
        | PROCESS_VM_WRITE
        | PROCESS_VM_READ,
        FALSE, processID);

    if (hProcess == NULL)
    {
        rc = GetLastError();
        _tprintf(_T("OpenProcess() failed\n"));
        return rc;
    }

    // load kernel32.dll
    HMODULE hKernel32 = LoadLibrary(_T("kernel32.dll"));

    // CreateRemoteThread()
    ht = CreateRemoteThread(
        hProcess,
        0,
        0,
        (DWORD(__stdcall *)(void*))GetProcAddress(hKernel32, "CloseHandle"),
        handle,
        0,
        &rc);

    if (ht == NULL)
    {
        //Something is wrong with the privileges, 
        //or the process doesn't like us
        rc = GetLastError();
        _tprintf(_T("CreateRemoteThread() failed\n"));
        goto cleanup;
    }

    switch (WaitForSingleObject(ht, 2000))
    {
    case WAIT_OBJECT_0:
        //Well done
        rc = 0;
        _tprintf(_T("Ok\n"), rc);
        break;

    default:
        //Oooops, shouldn't be here
        rc = GetLastError();
        _tprintf(_T("WaitForSingleObject() failed\n"));
        goto cleanup;
        break;
    }

cleanup:
    //Closes the remote thread handle
    CloseHandle(ht);

    //Free up the kernel32.dll
    if (hKernel32 != NULL)
        FreeLibrary(hKernel32);

    //Close the process handle
    CloseHandle(hProcess);

    return rc;
}

void WhoUsesFile(LPCTSTR lpFileName, BOOL bFullPathCheck, BOOL del)
{
    BOOL bShow = FALSE;
    CString name;
    CString processName;
    CString deviceFileName;
    CString fsFilePath;
    SystemProcessInformation::SYSTEM_PROCESS_INFORMATION* p;
    SystemProcessInformation pi;
    SystemHandleInformation hi;

    if (bFullPathCheck)
    {
        if (!SystemInfoUtils::GetDeviceFileName(lpFileName, deviceFileName))
        {
            _tprintf(_T("GetDeviceFileName() failed.\n"));
            return;
        }
    }

    hi.SetFilter(_T("File"), TRUE);

    if (hi.m_HandleInfos.GetHeadPosition() == NULL)
    {
        _tprintf(_T("No handle information\n"));
        return;
    }

    pi.Refresh();

    _tprintf(_T("%-6s  %-20s  %s\n"), _T("PID"), _T("Name"), _T("Path"));
    _tprintf(_T("------------------------------------------------------\n"));

    for (POSITION pos = hi.m_HandleInfos.GetHeadPosition(); pos != NULL; )
    {
        SystemHandleInformation::SYSTEM_HANDLE& h = hi.m_HandleInfos.GetNext(pos);

        if (pi.m_ProcessInfos.Lookup(h.ProcessID, p))
        {
            SystemInfoUtils::Unicode2CString(&p->usName, processName);
        }
        else
            processName = "";

        //NT4 Stupid thing if it is the services.exe and I call GetName :((
        if (_tcsicmp(processName, _T("services.exe")) == 0)
            continue;

        hi.GetName((HANDLE)h.HandleNumber, name, h.ProcessID);

        if (bFullPathCheck)
            bShow = _tcsstr(name, deviceFileName) != NULL;
        else
            bShow = _tcsstr(GetFileNamePosition(name), lpFileName) != NULL;

        if (bShow)
        {
            if (!bFullPathCheck)
            {
                fsFilePath = "";
                SystemInfoUtils::GetFsFileName(name, fsFilePath);
            }

            _tprintf(_T("%d  %-20s  %s\n"),
                h.ProcessID,
                processName,
                !bFullPathCheck ? fsFilePath : lpFileName);


            if (del == TRUE)
            {
                CloseRemoteHandle(h.ProcessID,(HANDLE)h.HandleNumber);
            }
        }

    }
}

void EnableDebugPriv(void)
{
    HANDLE hToken;
    LUID sedebugnameValue;
    TOKEN_PRIVILEGES tkp;

    // enable the SeDebugPrivilege
    if (!OpenProcessToken(GetCurrentProcess(),
        TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken))
    {
        _tprintf(_T("OpenProcessToken() failed, Error = %d SeDebugPrivilege is not available.\n"), GetLastError());
        return;
    }

    if (!LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &sedebugnameValue))
    {
        _tprintf(_T("LookupPrivilegeValue() failed, Error = %d SeDebugPrivilege is not available.\n"), GetLastError());
        CloseHandle(hToken);
        return;
    }

    tkp.PrivilegeCount = 1;
    tkp.Privileges[0].Luid = sedebugnameValue;
    tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

    if (!AdjustTokenPrivileges(hToken, FALSE, &tkp, sizeof tkp, NULL, NULL))
        _tprintf(_T("AdjustTokenPrivileges() failed, Error = %d SeDebugPrivilege is not available.\n"), GetLastError());

    CloseHandle(hToken);
}

void ShowUsage()
{
    _tprintf(_T("WhoUses 1.0 for www.codeguru.com\n"));
    _tprintf(_T("Written by Zoltan Csizmadia, zoltan_csizmadia@yahoo.com \n"));
    _tprintf(_T("\n"));
    _tprintf(_T("Usage: WhoUses.exe [/M] fileName\n"));
    _tprintf(_T("\n"));
    _tprintf(_T("          /M            fileName is a module name ( EXE, DLL, ... )\n"));
    _tprintf(_T("          fileName      File name\n"));
    _tprintf(_T("\n"));
    _tprintf(_T("Examples:\n"));
    _tprintf(_T("\n"));
    _tprintf(_T("          WhoUses.exe /M kernel32.dll\n"));
    _tprintf(_T("          WhoUses.exe /M c:\\test\\test.dll\n"));
    _tprintf(_T("          WhoUses.exe yourTextFile.txt\n"));
    _tprintf(_T("          WhoUses.exe c:\\pagefile.sys\n"));
    _tprintf(_T("          WhoUses.exe Serial0\n"));
}

int _tmain(int argc, TCHAR* argv[])
{
    ULONG nonSwitchCount = 0;
    BOOL bModule = FALSE;
    LPCTSTR lpPath = NULL;
    BOOL bFullPathCheck = FALSE;
    BOOL bUsage = TRUE;
    TCHAR lpFilePath[_MAX_PATH];
    BOOL del = FALSE;

    for (int i = 1; i < argc; i++)
    {
        if (_tcsicmp(argv[i], _T("/?")) == 0 || _tcsicmp(argv[i], _T("-?")) == 0)
        {
            bUsage = TRUE;
            break;
        }
        else if (_tcsicmp(argv[i], _T("/d")) == 0 || _tcsicmp(argv[i], _T("-d")) == 0)
        {
            del = TRUE;
        }
        else if (_tcsicmp(argv[i], _T("/m")) == 0 || _tcsicmp(argv[i], _T("-m")) == 0)
        {
            bModule = TRUE;
        }
        else
        {
            if (nonSwitchCount != 0)
            {
                bUsage = TRUE;
                break;
            }

            lpPath = argv[i];

            bUsage = FALSE;

            nonSwitchCount++;
        }
    }

    if (bUsage)
    {
        ShowUsage();
        return -1;
    }

    EnableDebugPriv();

    bFullPathCheck = GetFileNamePosition(lpPath) != lpPath;

    if (bFullPathCheck)
    {
        if (GetFullPathName(lpPath, _MAX_PATH, lpFilePath, NULL) == 0)
        {
            _tprintf(_T("GetFullPathName() failed. Error = %d\n"), GetLastError());
            return -2;
        }
    }
    else
        _tcscpy_s(lpFilePath, GetFileNamePosition(lpPath));

    if (bModule)
        WhoUsesModule(lpFilePath, bFullPathCheck);
    else
        WhoUsesFile(lpFilePath, bFullPathCheck,del);

    return 0;
}

