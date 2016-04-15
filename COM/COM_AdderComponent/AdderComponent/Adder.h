// Adder.h: Definition of the Adder class
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ADDER_H__91C28FF9_22BC_47A7_9570_B5C549FD434C__INCLUDED_)
#define AFX_ADDER_H__91C28FF9_22BC_47A7_9570_B5C549FD434C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// Adder

class Adder : 
	public IDispatchImpl<IAdder, &IID_IAdder, &LIBID_ADDERCOMPONENTLib>, 
	public ISupportErrorInfo,
	public CComObjectRoot,
	public CComCoClass<Adder,&CLSID_Adder>
{
public:
	Adder() {}
BEGIN_COM_MAP(Adder)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IAdder)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
END_COM_MAP()
//DECLARE_NOT_AGGREGATABLE(Adder) 
// Remove the comment from the line above if you don't want your object to 
// support aggregation. 

DECLARE_REGISTRY_RESOURCEID(IDR_Adder)
// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// IAdder
public:
	STDMETHOD(Mul)(int n1,int n2,int* result);
	STDMETHOD(Sub)(int n1,int n2,int* result);
	STDMETHOD(Add)(int n1,int n2,int* result);
};

#endif // !defined(AFX_ADDER_H__91C28FF9_22BC_47A7_9570_B5C549FD434C__INCLUDED_)
