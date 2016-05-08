using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Net.FtpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new ConsoleTraceListener());
            try
            {
                Examples.OpenReadExample.OpenRead();

                //BeginConnectExample.BeginConnect();
                //BeginCreateDirectoryExample.BeginCreateDirectory();
                //BeginDeleteDirectoryExample.BeginDeleteDirectory();
                //BeginDeleteFileExample.BeginDeleteFile();
                //BeginDirectoryExistsExample.BeginDirectoryExists();
                //BeginDisconnectExample.BeginDisconnect();
                //BeginDownloadExample.BeginDownload();
                //BeginExecuteExample.BeginExecute();
                //BeginFileExistsExample.BeginFileExists();
                //BeginGetFileSizeExample.BeginGetFileSize();
                //BeginGetListing.BeginGetListingExample();
                //BeginGetModifiedTimeExample.BeginGetModifiedTime();
                //BeginGetWorkingDirectoryExample.BeginGetWorkingDirectory();
                //BeginRenameExample.BeginRename();
                //BeginSetDataTypeExample.BeginSetDataType();
                //BeginSetWorkingDirectoryExample.BeginSetWorkingDirectory();
                //OpenReadURI.OpenURI();
                //BeginGetNameListingExample.BeginGetNameListing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
