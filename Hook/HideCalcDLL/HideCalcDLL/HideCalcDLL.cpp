// HideNotepadDLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "HideCalcDLL.h"


// This is an example of an exported variable
HIDECALCDLL_API int nHideCalcDLL=0;

// This is an example of an exported function.
HIDECALCDLL_API int fnHideCalcDLL(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see HideCalcDLL.h for the class definition
CHideCalcDLL::CHideCalcDLL()
{
    return;
}
