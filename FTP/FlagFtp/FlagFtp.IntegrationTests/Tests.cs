using System;
using System.IO;
using System.Net;

namespace FlagFtp.IntegrationTests
{
    internal class Tests
    {
        private static FtpClient client;

        private static void Main(string[] args)
        {
            Console.WriteLine("Reading login data...");

            string hostAddress = "ftp://ftp.adobe.com";
            string username = "anonymous";
            string password = "";

            client = new FtpClient(new NetworkCredential(username, password));
            var files = client.GetFiles(new Uri(new Uri(hostAddress), "./"));
            foreach (var item in files)
            {
                Console.WriteLine(item.FullName);
                var ffs = client.OpenRead(item);
                FileStream ws = File.Create(Directory.GetCurrentDirectory() + "\\" + item.Name);
                int value = 0;
                value = ffs.ReadByte();
                while (value != -1)
                {
                    ws.WriteByte((byte)value);
                    value = ffs.ReadByte();
                }
                ws.Close();
                ffs.Close();

                Console.WriteLine("\t" + item.Uri.AbsolutePath);
            }

            var dirs = client.GetDirectories(new Uri(new Uri(hostAddress), "./"));
            foreach (var item in dirs)
            {
                Console.WriteLine(item.FullName);
                Console.WriteLine("\t" + item.Uri.AbsolutePath);
            }
            //client.OpenRead()








            //CreateDirectoryTest(new Uri(new Uri(hostAddress), "/TestDirectory1"));
            //DeleteDirectoryTest(new Uri(new Uri(hostAddress), "/TestDirectory1"));

            Console.WriteLine();
            Console.WriteLine("Integration test finished.");
            Console.ReadLine();






        }

        private static void CreateDirectoryTest(Uri directory)
        {
            client.CreateDirectory(directory);

            bool exists = client.DirectoryExists(directory);
            Console.WriteLine("CreateDirectory method has " + (exists ? "SUCCEED" : "FAILED"));
        }

        private static void DeleteDirectoryTest(Uri directory)
        {
            bool succeed = false;

            if (client.DirectoryExists(directory))
            {
                client.DeleteDirectory(directory);

                succeed = !client.DirectoryExists(directory);
            }

            Console.WriteLine("CreateDirectory method has " + (succeed ? "SUCCEED" : "FAILED"));
        }
    }
}