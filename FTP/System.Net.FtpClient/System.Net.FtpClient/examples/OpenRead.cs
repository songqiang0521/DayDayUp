using System;
using System.IO;
using System.Net;
using System.Net.FtpClient;

namespace Examples
{
    public class OpenReadExample
    {
        public static void OpenRead()
        {
            using (FtpClient conn = new FtpClient())
            {
                conn.Host = "ftp.adobe.com";
                conn.Credentials = new NetworkCredential("anonymous", "songqiang0521@13.com");

                using (Stream istream = conn.OpenRead("/license.txt"))
                {
                    try
                    {
                        var fs = File.OpenWrite("license.txt");

                        int value = 0;
                        value = istream.ReadByte();
                        while (value != -1)
                        {
                            fs.WriteByte((byte)value);
                            value = istream.ReadByte();
                        }
                        fs.Close();

                        // istream.Position is incremented accordingly to the reads you perform
                        // istream.Length == file size if the server supports getting the file size
                        // also note that file size for the same file can vary between ASCII and Binary
                        // modes and some servers won't even give a file size for ASCII files! It is
                        // recommended that you stick with Binary and worry about character encodings
                        // on your end of the connection.
                    }
                    finally
                    {
                        Console.WriteLine();
                        istream.Close();
                    }
                }
            }
        }
    }
}

