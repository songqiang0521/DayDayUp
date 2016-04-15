/* this file contains the actual definitions of */
/* the IIDs and CLSIDs */

/* link this file in with the server and any clients */


/* File created by MIDL compiler version 5.01.0164 */
/* at Thu Apr 30 09:25:19 2015
 */
/* Compiler settings for C:\SQ\AdderComponent\AdderComponent.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )
#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

const IID IID_IAdder = {0xB626FAC9,0xAB97,0x4409,{0xA4,0x13,0xA3,0x1E,0xC3,0x07,0xF2,0x5F}};


const IID LIBID_ADDERCOMPONENTLib = {0x4AC2AC57,0xAF56,0x46D5,{0x83,0xEE,0x7D,0x69,0x04,0x52,0xB6,0x68}};


const CLSID CLSID_Adder = {0xB3C114FE,0x5A60,0x49E7,{0xA9,0x46,0xCF,0x45,0xC9,0xD1,0xD5,0x9D}};


#ifdef __cplusplus
}
#endif

