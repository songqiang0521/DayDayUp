// Adder.cpp : Implementation of CAdderComponentApp and DLL registration.

#include "stdafx.h"
#include "AdderComponent.h"
#include "Adder.h"

/////////////////////////////////////////////////////////////////////////////
//

STDMETHODIMP Adder::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IAdder,
	};

	for (int i=0;i<sizeof(arr)/sizeof(arr[0]);i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

STDMETHODIMP Adder::Add(int n1, int n2, int *result)
{
	// TODO: Add your implementation code here

	*result=n1+n2;
	
	return S_OK;
}

STDMETHODIMP Adder::Sub(int n1, int n2, int *result)
{
	// TODO: Add your implementation code here

	*result=n1-n2;
	return S_OK;
}

STDMETHODIMP Adder::Mul(int n1, int n2, int *result)
{
	// TODO: Add your implementation code here

	*result=n1*n2;
	return S_OK;
}
