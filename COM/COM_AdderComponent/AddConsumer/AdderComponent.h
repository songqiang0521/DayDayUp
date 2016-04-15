/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Thu Apr 30 09:25:19 2015
 */
/* Compiler settings for C:\SQ\AdderComponent\AdderComponent.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __AdderComponent_h__
#define __AdderComponent_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IAdder_FWD_DEFINED__
#define __IAdder_FWD_DEFINED__
typedef interface IAdder IAdder;
#endif 	/* __IAdder_FWD_DEFINED__ */


#ifndef __Adder_FWD_DEFINED__
#define __Adder_FWD_DEFINED__

#ifdef __cplusplus
typedef class Adder Adder;
#else
typedef struct Adder Adder;
#endif /* __cplusplus */

#endif 	/* __Adder_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IAdder_INTERFACE_DEFINED__
#define __IAdder_INTERFACE_DEFINED__

/* interface IAdder */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IAdder;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B626FAC9-AB97-4409-A413-A31EC307F25F")
    IAdder : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Add( 
            int n1,
            int n2,
            int __RPC_FAR *result) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Sub( 
            int n1,
            int n2,
            int __RPC_FAR *result) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Mul( 
            int n1,
            int n2,
            int __RPC_FAR *result) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IAdderVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IAdder __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IAdder __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IAdder __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            IAdder __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            IAdder __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            IAdder __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            IAdder __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Add )( 
            IAdder __RPC_FAR * This,
            int n1,
            int n2,
            int __RPC_FAR *result);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Sub )( 
            IAdder __RPC_FAR * This,
            int n1,
            int n2,
            int __RPC_FAR *result);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Mul )( 
            IAdder __RPC_FAR * This,
            int n1,
            int n2,
            int __RPC_FAR *result);
        
        END_INTERFACE
    } IAdderVtbl;

    interface IAdder
    {
        CONST_VTBL struct IAdderVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IAdder_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IAdder_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IAdder_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IAdder_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IAdder_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IAdder_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IAdder_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IAdder_Add(This,n1,n2,result)	\
    (This)->lpVtbl -> Add(This,n1,n2,result)

#define IAdder_Sub(This,n1,n2,result)	\
    (This)->lpVtbl -> Sub(This,n1,n2,result)

#define IAdder_Mul(This,n1,n2,result)	\
    (This)->lpVtbl -> Mul(This,n1,n2,result)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IAdder_Add_Proxy( 
    IAdder __RPC_FAR * This,
    int n1,
    int n2,
    int __RPC_FAR *result);


void __RPC_STUB IAdder_Add_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IAdder_Sub_Proxy( 
    IAdder __RPC_FAR * This,
    int n1,
    int n2,
    int __RPC_FAR *result);


void __RPC_STUB IAdder_Sub_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IAdder_Mul_Proxy( 
    IAdder __RPC_FAR * This,
    int n1,
    int n2,
    int __RPC_FAR *result);


void __RPC_STUB IAdder_Mul_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IAdder_INTERFACE_DEFINED__ */



#ifndef __ADDERCOMPONENTLib_LIBRARY_DEFINED__
#define __ADDERCOMPONENTLib_LIBRARY_DEFINED__

/* library ADDERCOMPONENTLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ADDERCOMPONENTLib;

EXTERN_C const CLSID CLSID_Adder;

#ifdef __cplusplus

class DECLSPEC_UUID("B3C114FE-5A60-49E7-A946-CF45C9D1D59D")
Adder;
#endif
#endif /* __ADDERCOMPONENTLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
