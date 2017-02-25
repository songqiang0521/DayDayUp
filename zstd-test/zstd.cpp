// zstd.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>
//#include "zstd.h"
//#pragma comment(lib,"libzstd.lib")

/***************************************
*  Simple API
***************************************/
/*! ZSTD_compress() :
Compresses `src` content as a single zstd compressed frame into already allocated `dst`.
Hint : compression runs faster if `dstCapacity` >=  `ZSTD_compressBound(srcSize)`.
@return : compressed size written into `dst` (<= `dstCapacity),
or an error code if it fails (which can be tested using ZSTD_isError()). */


//void*, size_t, const void*, size_t, int
typedef size_t(*ZSTD_compress)(void* dst, size_t dstCapacity,const void* src, size_t srcSize,int compressionLevel);



/*! ZSTD_decompress() :
`compressedSize` : must be the _exact_ size of a single compressed frame.
`dstCapacity` is an upper bound of originalSize.
If user cannot imply a maximum upper bound, it's better to use streaming mode to decompress data.
@return : the number of bytes decompressed into `dst` (<= `dstCapacity`),
or an errorCode if it fails (which can be tested using ZSTD_isError()). */

typedef size_t(*ZSTD_decompress)(void*, size_t, const void*, size_t);


/*! ZSTD_getDecompressedSize() :
*   'src' is the start of a zstd compressed frame.
*   @return : content size to be decompressed, as a 64-bits value _if known_, 0 otherwise.
*    note 1 : decompressed size is an optional field, that may not be present, especially in streaming mode.
*             When `return==0`, data to decompress could be any size.
*             In which case, it's necessary to use streaming mode to decompress data.
*             Optionally, application can still use ZSTD_decompress() while relying on implied limits.
*             (For example, data may be necessarily cut into blocks <= 16 KB).
*    note 2 : decompressed size is always present when compression is done with ZSTD_compress()
*    note 3 : decompressed size can be very large (64-bits value),
*             potentially larger than what local system can handle as a single memory segment.
*             In which case, it's necessary to use streaming mode to decompress data.
*    note 4 : If source is untrusted, decompressed size could be wrong or intentionally modified.
*             Always ensure result fits within application's authorized limits.
*             Each application can set its own limits.
*    note 5 : when `return==0`, if precise failure cause is needed, use ZSTD_getFrameParams() to know more. */

typedef size_t(*ZSTD_getDecompressedSize)(const void*, size_t);



class ZSTD
{
public:
    ZSTD();
    ~ZSTD();
    ZSTD_getDecompressedSize _getDecompressedSize;
    ZSTD_decompress _decompress;
    ZSTD_compress _compress;


private:
    
    HMODULE _hModule;

};

ZSTD::ZSTD()
{
    _hModule = LoadLibrary("libzstd.dll");

    if (_hModule != NULL)
    {
        _getDecompressedSize = (ZSTD_getDecompressedSize)GetProcAddress(_hModule, "ZSTD_getDecompressedSize");
        _decompress = (ZSTD_decompress)GetProcAddress(_hModule, "ZSTD_decompress");
        _compress = (ZSTD_compress)GetProcAddress(_hModule, "ZSTD_compress");
    }
    else
    {
        printf("last error is %d\n", GetLastError());
   

    }


}

ZSTD::~ZSTD()
{
    if (_hModule != NULL)
    {
        FreeLibrary(_hModule);
    }
}


struct TagItemData
{
    short length;
    short type;
    float value;
    short status;
};


int main()
{
    const int count = 10000;
    int len = count * sizeof(TagItemData);


    char* buffer = new char[len];
    TagItemData* data=(TagItemData*)buffer;
    
    for (size_t i = 0; i < count; i++)
    {
        data[i].length = 0;
        data[i].status =  1;
        data[i].type = i + 2;
        data[i].value = 0;
    }

   



    ZSTD* zstd = new ZSTD();


    //size_t size_needed= ZSTD_compressBound(len);
 char* dstBuffer = new char[len];
    memset(dstBuffer, 0, len);
    //size_t dstSize = ZSTD_compress(dstBuffer, size_needed, buffer, len, 10);

    size_t dstSize=zstd->_compress(dstBuffer, len, buffer, len, 3);



    char* tobeFilled = new char[len];
    size_t filledSize=zstd->_decompress(tobeFilled, len, dstBuffer, dstSize);
    //size_t filledSize= ZSTD_decompress(tobeFilled, len, dstBuffer, dstSize);


    return 0;
}

