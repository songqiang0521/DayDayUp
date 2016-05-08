using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Specialized;

namespace File_Directory
{
    class Program
    {

        static void Main(string[] args)
        {

            string path = @"Z:\";
            string pattern = @"*";
            var di = new DirectoryInfo(path);
            var dirs = di.GetDirectories(pattern, SearchOption.AllDirectories);
            foreach (var item in dirs)
            {
                Console.WriteLine(item.FullName);
            }

            var files = di.GetFiles(pattern, SearchOption.AllDirectories);
            foreach (var item in files)
            {
                Console.WriteLine(item.FullName);
            }

            Console.Write("\n");

            
           string filePathDir= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
           string filePath = Path.Combine(filePathDir, "sq.txt");
           Console.WriteLine();

           

           FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate|FileMode.Append, FileAccess.Write, FileShare.None);
           //var stream = new StreamReader(fs);
            //byte[] array=new byte[1024];
            //new Random().NextBytes(array);
            //fs.Write(array, 0, array.Length);
          
           var stream2 = new StreamWriter(fs,Encoding.Default);
           stream2.WriteLine("12345789");
           stream2.WriteLine("123");
           
           stream2.Flush();
          //var str= stream.ReadToEnd();
          //Console.WriteLine(str);

















































            //DriveInfo di = new DriveInfo("C");
            //Console.WriteLine(di.TotalSize/1024/1024/1024);


            /*
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo  driver in drivers)
            {
                Console.WriteLine("文件系统名称"+driver.DriveFormat);
                Console.WriteLine("驱动器类型"+driver.DriveType);
                Console.WriteLine("驱动器可用总量"+driver.TotalFreeSpace/1024/1024/1024+"G");
                Console.WriteLine("驱动器上可用空闲空间"+driver.AvailableFreeSpace/1024/1024/1024+"G");
                Console.WriteLine("驱动器是否已经准备好{0}",(driver.IsReady)?"是":"否");
                Console.WriteLine("驱动器名称"+driver.Name);
                Console.WriteLine("驱动器的根目录"+driver.RootDirectory);
                Console.WriteLine("卷标"+driver.VolumeLabel);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

            }
            */



































            /*
            string path=@"D:\sq.txt";
            FileInfo fi = new FileInfo(path);
            FileStream fs = new FileStream(@"D:\sq.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr1 = new StreamReader(fs,Encoding.Unicode);
            StringCollection result = new StringCollection();
            string line;
         
            while ((line=sr1.ReadLine())!=null)
            {
                result.Add(line);   
            }
               string[] lines=new string[result.Count];
            Console.WriteLine(result.ToString());
            result.CopyTo(lines, 0);
            foreach (string str in lines)
            {
                Console.WriteLine(str);
                
            }
            sr1.Close();
            sr1.Close();
            */
























            /*
            int nCols = 16;
            FileStream fs = new FileStream(@"D:\sq.txt", FileMode.OpenOrCreate, FileAccess.Read);
            long  readsLength = fs.Length;
            if (readsLength>65536/4)
            {
                readsLength = 65536 / 4;
            }

            int nLines = (int)(readsLength / nCols) + 1;
            string[] Lines=new string[nLines];
            int nBytesRead = 0;
            for (int i = 0; i < nLines; i++)
            {
                StringBuilder Line = new StringBuilder();
                Line.Capacity = 4 * nCols;

                for (int j = 0; j < nCols; j++)
                {
                    int nextByte = fs.ReadByte();
                    nBytesRead++;
                    if (nBytesRead<0||nBytesRead>65526)
                    {
                        break;
                        
                    }
                    char nextChar = (char)nextByte;
                    if (nextChar<16)
                    {
                        Line.Append("x0"+string.Format("{0,1:X}",(int)(nextChar)));
                        
                    }
                    else if(char.IsLetterOrDigit(nextChar)||char.IsPunctuation(nextChar))
                    {
                        Line.Append("  "+nextChar+"  ");


                    }
                    else
                    {
                        Line.Append(" x"+string.Format("{0,2:X}",(int)(nextChar)));
                    }

                    Lines[i]=Line. ToString();


                }
               
            }


            fs.Close();
            foreach (string item in Lines)
            {
                Console.WriteLine(item);
                
            }
            */










            //FileInfo fileInfo = new FileInfo(@"D:\sq.txt");
            //FileStream fs1 = fileInfo.OpenRead();
            //byte[] bytes=new byte[fileInfo.Length];
            //fs1.Read(bytes, 3, (int)fileInfo.Length-4);
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    Console.WriteLine(bytes[i]);

            //}

            //string filePath = @"D:\sq.txt";
            //string strs = File.ReadAllText(filePath);
            //Console.WriteLine(strs);
            //string[] strs = File.ReadAllLines(filePath,Encoding.Default);
            //foreach (string str in strs)
            //{
            //    Console.WriteLine(str);

            //}

            //Console.WriteLine(strs[0]);





            /*
            FileInfo myFileInfo = new FileInfo(@"D:\sq.txt");
            Console.WriteLine(myFileInfo.Exists);
            Console.WriteLine("文件创建日期"+myFileInfo.CreationTime);
            Console.WriteLine("文件名"+myFileInfo.FullName);
            Console.WriteLine("文件大小"+myFileInfo.Length);
            myFileInfo.CreationTime = new DateTime(2001, 1, 1);
            Console.WriteLine("文件创建日期" + myFileInfo.CreationTime);
           */

            







            Console.ReadKey();
        }
    }
}
