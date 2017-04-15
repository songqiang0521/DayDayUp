// GetDiskSerialNumber.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>



typedef enum _STORAGE_PROPERTY_ID {
    StorageDeviceProperty = 0,
		StorageAdapterProperty
} STORAGE_PROPERTY_ID, *PSTORAGE_PROPERTY_ID;

typedef enum _STORAGE_QUERY_TYPE {
    PropertyStandardQuery = 0,          // Retrieves the descriptor
		PropertyExistsQuery,                // Used to test whether the descriptor is supported
		PropertyMaskQuery,                  // Used to retrieve a mask of writeable fields in the descriptor
		PropertyQueryMaxDefined     // use to validate the value
} STORAGE_QUERY_TYPE, *PSTORAGE_QUERY_TYPE;

typedef struct _STORAGE_PROPERTY_QUERY {
	
    //
    // ID of the property being retrieved
    //
	
    STORAGE_PROPERTY_ID PropertyId;
	
    //
    // Flags indicating the type of query being performed
    //
	
    STORAGE_QUERY_TYPE QueryType;
	
    //
    // Space for additional parameters if necessary
    //
	
    UCHAR AdditionalParameters[1];
	
} STORAGE_PROPERTY_QUERY, *PSTORAGE_PROPERTY_QUERY;



#define METHOD_BUFFERED                 0
#define FILE_ANY_ACCESS                 0
#define FILE_DEVICE_MASS_STORAGE        0x0000002d
#define IOCTL_STORAGE_BASE FILE_DEVICE_MASS_STORAGE

#define CTL_CODE( DeviceType, Function, Method, Access ) (                 \
    ((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2) | (Method) \
)
#define IOCTL_STORAGE_QUERY_PROPERTY   CTL_CODE(IOCTL_STORAGE_BASE, 0x0500, METHOD_BUFFERED, FILE_ANY_ACCESS)



typedef enum _STORAGE_BUS_TYPE {
    BusTypeUnknown = 0x00,
		BusTypeScsi,
		BusTypeAtapi,
		BusTypeAta,
		BusType1394,
		BusTypeSsa,
		BusTypeFibre,
		BusTypeUsb,
		BusTypeRAID,
		BusTypeMaxReserved = 0x7F
} STORAGE_BUS_TYPE, *PSTORAGE_BUS_TYPE;

typedef struct _STORAGE_DEVICE_DESCRIPTOR {
	
    //
    // Sizeof(STORAGE_DEVICE_DESCRIPTOR)
    //
	
    ULONG Version;
	
    //
    // Total size of the descriptor, including the space for additional
    // data and id strings
    //
	
    ULONG Size;
	
    //
    // The SCSI-2 device type
    //
	
    UCHAR DeviceType;
	
    //
    // The SCSI-2 device type modifier (if any) - this may be zero
    //
	
    UCHAR DeviceTypeModifier;
	
    //
    // Flag indicating whether the device's media (if any) is removable.  This
    // field should be ignored for media-less devices
    //
	
    BOOLEAN RemovableMedia;
	
    //
    // Flag indicating whether the device can support mulitple outstanding
    // commands.  The actual synchronization in this case is the responsibility
    // of the port driver.
    //
	
    BOOLEAN CommandQueueing;
	
    //
    // Byte offset to the zero-terminated ascii string containing the device's
    // vendor id string.  For devices with no such ID this will be zero
    //
	
    ULONG VendorIdOffset;
	
    //
    // Byte offset to the zero-terminated ascii string containing the device's
    // product id string.  For devices with no such ID this will be zero
    //
	
    ULONG ProductIdOffset;
	
    //
    // Byte offset to the zero-terminated ascii string containing the device's
    // product revision string.  For devices with no such string this will be
    // zero
    //
	
    ULONG ProductRevisionOffset;
	
    //
    // Byte offset to the zero-terminated ascii string containing the device's
    // serial number.  For devices with no serial number this will be zero
    //
	
    ULONG SerialNumberOffset;
	
    //
    // Contains the bus type (as defined above) of the device.  It should be
    // used to interpret the raw device properties at the end of this structure
    // (if any)
    //
	
    STORAGE_BUS_TYPE BusType;
	
    //
    // The number of bytes of bus-specific data which have been appended to
    // this descriptor
    //
	
   ULONG RawPropertiesLength;
	
    //
    // Place holder for the first byte of the bus specific property data
    //
	
    UCHAR RawDeviceProperties[1];
	
} STORAGE_DEVICE_DESCRIPTOR, *PSTORAGE_DEVICE_DESCRIPTOR;



//  function to decode the serial numbers of IDE hard drives
//  using the IOCTL_STORAGE_QUERY_PROPERTY command 
char * EflipAndCodeBytes (const char * str,
						 int pos,
						 int flip,
						 char * buf)
{
	int i;
	int j = 0;
	int k = 0;
	
	buf [0] = '\0';
	if (pos <= 0)
		return buf;
	
	if ( ! j)
	{
		char p = 0;
		
		// First try to gather all characters representing hex digits only.
		j = 1;
		k = 0;
		buf[k] = 0;
		for (i = pos; j && str[i] != '\0'; ++i)
		{
			char c = tolower(str[i]);
			
			if (isspace(c))
				c = '0';
			
			++p;
			buf[k] <<= 4;
			
			if (c >= '0' && c <= '9')
				buf[k] |= (unsigned char) (c - '0');
			else if (c >= 'a' && c <= 'f')
				buf[k] |= (unsigned char) (c - 'a' + 10);
			else
			{
				j = 0;
				break;
			}
			
			if (p == 2)
			{
				if (buf[k] != '\0' && ! isprint(buf[k]))
				{
					j = 0;
					break;
				}
				++k;
				p = 0;
				buf[k] = 0;
			}
			
		}
	}
	
	if ( ! j)
	{
		// There are non-digit characters, gather them as is.
		j = 1;
		k = 0;
		for (i = pos; j && str[i] != '\0'; ++i)
		{
			char c = str[i];
			
			if ( ! isprint(c))
			{
				j = 0;
				break;
			}
			
			buf[k++] = c;
		}
	}
	
	if ( ! j)
	{
		// The characters are not there or are not printable.
		k = 0;
	}
	
	buf[k] = '\0';
	
	if (flip)
		// Flip adjacent characters
		for (j = 0; j < k; j += 2)
		{
			char t = buf[j];
			buf[j] = buf[j + 1];
			buf[j + 1] = t;
		}
		
		// Trim any beginning and end space
		i = j = -1;
		for (k = 0; buf[k] != '\0'; ++k)
		{
			if (! isspace(buf[k]))
			{
				if (i < 0)
					i = k;
				j = k;
			}
		}
		
		if ((i >= 0) && (j >= 0))
		{
			for (k = i; (k <= j) && (buf[k] != '\0'); ++k)
				buf[k - i] = buf[k];
			buf[k - i] = '\0';
		}
		
		return buf;
}



int main(int argc, char* argv[])
{
	

	char pChID[256]={0};
	
	BOOL done = FALSE;
	int drive = 0;
	
	int MAX_IDE_DRIVES=16;
	
	for (drive = 0; drive < MAX_IDE_DRIVES; drive++)
	{
		HANDLE hPhysicalDriveIOCTL = 0;
		
		//  Try to get a handle to PhysicalDrive IOCTL, report failure
		//  and exit if can't.
		char driveName [MAX_PATH];
		
		sprintf (driveName, "\\\\.\\PhysicalDrive%d", drive);
		
		//  Windows NT, Windows 2000, Windows XP - admin rights not required
		hPhysicalDriveIOCTL = CreateFile (driveName, 0,
			FILE_SHARE_READ | FILE_SHARE_WRITE, NULL,
			OPEN_EXISTING, 0, NULL);
		if (hPhysicalDriveIOCTL == INVALID_HANDLE_VALUE)
		{
			continue ;
		}
		else
		{
			STORAGE_PROPERTY_QUERY query;
			DWORD cbBytesReturned = 0;
			char local_buffer [10000];
			
			memset ((void *) & query, 0, sizeof (query));
			query.PropertyId = StorageDeviceProperty;
			query.QueryType = PropertyStandardQuery;
			
			memset (local_buffer, 0, sizeof (local_buffer));
			
			if ( DeviceIoControl (hPhysicalDriveIOCTL, IOCTL_STORAGE_QUERY_PROPERTY,
				& query,
				sizeof (query),
				& local_buffer [0],
				sizeof (local_buffer),
				& cbBytesReturned, NULL) )
			{         
				STORAGE_DEVICE_DESCRIPTOR * descrip = (STORAGE_DEVICE_DESCRIPTOR *) & local_buffer;
				char serialNumber [1000];
				char modelNumber [1000];
				char vendorId [1000];
				char productRevision [1000];
				
				EflipAndCodeBytes (local_buffer,
					descrip -> VendorIdOffset,
					0, vendorId );
				printf("venderID:%s\n",vendorId);



				EflipAndCodeBytes (local_buffer,
					descrip -> ProductIdOffset,
					0, modelNumber );
				printf("modelNumber:%s\n",modelNumber);



				EflipAndCodeBytes (local_buffer,
					descrip -> ProductRevisionOffset,
					0, productRevision );
				printf("productRevision%s\n",productRevision);


				EflipAndCodeBytes (local_buffer,
					descrip -> SerialNumberOffset,
					1, serialNumber );
				printf("serialNumber%s\n",serialNumber);


				printf("\n\n");

				
				if (0 == pChID [0] &&
					//  serial number must be alphanumeric
					//  (but there can be leading spaces on IBM drives)
					(isalnum (serialNumber [0]) || isalnum (serialNumber [19])))
				{
					strcpy (pChID, serialNumber);
					//	strcpy (HardDriveModelNumber, modelNumber);
					done = TRUE;
				}
				
				// Get the disk drive geometry.
				
			}
			else
			{
				DWORD err = GetLastError ();
				
			}
			
			CloseHandle (hPhysicalDriveIOCTL);
		}
	}

	getchar();


	return 0;
}

