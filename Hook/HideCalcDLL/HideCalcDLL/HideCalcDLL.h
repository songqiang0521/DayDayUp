// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the HIDENOTEPADDLL_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// HIDECALCDLL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef HIDECALCDLL_EXPORTS
#define HIDECALCDLL_API __declspec(dllexport)
#else
#define HIDECALCDLL_API __declspec(dllimport)
#endif

// This class is exported from the HideNotepadDLL.dll
class HIDECALCDLL_API CHideCalcDLL {
public:
	CHideCalcDLL(void);
	// TODO: add your methods here.
};

extern HIDECALCDLL_API int nHideCalcDLL;

HIDECALCDLL_API int fnHideCalcDLL(void);
