// AddConsumer.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "AdderComponent.h"

int main(int argc, char* argv[])
{
	printf("Hello World!\n");
	CoInitialize(NULL);
	
	IAdder* pAdder=NULL;
	
	IUnknown* ppv=NULL;
	
	
	CLSID clsid; 
	
	HRESULT hr = CLSIDFromProgID(L"AdderComponent.Adder", &clsid); 
	hr=CoCreateInstance(CLSID_Adder,NULL,CLSCTX_INPROC_SERVER,IID_IUnknown,(LPVOID*)&ppv);
	
	//hr=CoCreateInstance(clsid,NULL,CLSCTX_INPROC_SERVER,IID_IUnknown,(LPVOID*)&ppv);
	if (FAILED(hr))
	{
		int er=GetLastError();
	}


	ppv->QueryInterface(IID_IAdder,(LPVOID *)&pAdder);
	int result=0;
	
	pAdder->Add(10,20,&result);
	pAdder->Sub(20,10,&result);
	pAdder->Mul(10,20,&result);

	
	
	return 0;
}

