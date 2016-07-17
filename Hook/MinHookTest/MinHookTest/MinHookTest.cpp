// MinHookTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>
#include "MinHook.h"

#if defined _M_X64
#pragma comment(lib, "libMinHook.x64.lib")

#elif defined _M_IX86
//静态库的链接，适用于hook自己进程中的函数。
//无需加载MinHookx86.dll即可生效，
//因为这些方法已经被链接到exe当中了。
#pragma comment(lib, "MinHook.x86.lib")

#endif



typedef int (WINAPI *MESSAGEBOXW)(HWND, LPCWSTR, LPCWSTR, UINT);

// 因为还要在hook中调用原始的函数，所以这个字段用来保存hook时返回的原始函数地址
MESSAGEBOXW fpMessageBoxW_org = NULL;

// Detour function which overrides MessageBoxW.
int WINAPI MessageBoxW_detour(HWND hWnd, LPCWSTR lpText, LPCWSTR lpCaption, UINT uType)
{
    return fpMessageBoxW_org(hWnd, L"Hooked!", lpCaption, uType);
}

int main()
{
    // Initialize MinHook.
    if (MH_Initialize() != MH_OK)
    {
        return 1;
    }

    MH_STATUS state = MH_STATUS::MH_UNKNOWN;

    // Create hook
    state = MH_CreateHook(&MessageBoxW, &MessageBoxW_detour, (LPVOID*)(&fpMessageBoxW_org));
    if (state != MH_STATUS::MH_OK)
    {
        int xx = 0;
        xx = 0;
    }

    // Enable hook
    if (MH_EnableHook(&MessageBoxW) != MH_OK)
    {
        return 1;
    }

    int ret = MessageBoxW(NULL, _T("not hooked"), _T("minhook sample"), MB_OK);


    // Disable the hook for MessageBoxW.
    if (MH_DisableHook(&MessageBoxW) != MH_OK)
    {
        return 1;
    }

    // Expected to tell "Not hooked...".
    MessageBoxW(NULL, L"Not hooked...", L"MinHook Sample", MB_OK);

    // Uninitialize MinHook.
    if (MH_Uninitialize() != MH_OK)
    {
        return 1;
    }

    return 0;
}

