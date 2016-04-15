using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.AccessControl;
using System.IO;
using System.Security.Principal;
using Microsoft.Win32;


namespace 文件ACL
{
    class Program
    {


        //static  string path = @"D:\sq.txt";
        //static string dir = @"C:\";

        static void Main(string[] args)
        {

            //注册表不区分大小写么？？？？？？？？？？？？
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey software = hklm.OpenSubKey("software");
            Console.WriteLine("打开sortware" + software.Name);

            RegistryKey Mine = software.CreateSubKey("SQ");
            Mine.SetValue("SQ-String项", "Hello,World!");
            Console.WriteLine(Mine.GetValue("SQ-String项"));
            Mine.SetValue("SQ-Int项", 1989624);

            //Mine.OpenSubKey("SQ", true);
            //Mine.DeleteSubKey("SQ");
            Console.WriteLine(Mine.GetValue("SQ-int项"));
            Console.WriteLine(Mine.GetValue("SQ-Int项"));

            Console.WriteLine("子健的个数" + Mine.SubKeyCount.ToString());
            Console.WriteLine("值的个数" + Mine.ValueCount.ToString());


            













            #region
            /*
            DirectoryInfo dI = new DirectoryInfo(dir);

            if (dI.Exists)
            {
                DirectorySecurity dirSecu = dI.GetAccessControl();
                foreach (FileSystemAccessRule  item in dirSecu.GetAccessRules(true ,true,typeof (NTAccount)))
                {
                    Console.WriteLine(item.AccessControlType+item.IdentityReference.ToString());
                    
                }
                
            }

            */
            #endregion




            //string path = @"D:\sq.txt";
            //using(FileStream fs=new FileStream (path,FileMode.Open,FileAccess.Read))
            //{
            //    FileSecurity fileSecu = fs.GetAccessControl();

            //    foreach (FileSystemAccessRule  item in fileSecu.GetAccessRules(true ,true ,typeof (NTAccount)))
            //    {
            //        Console.WriteLine(item.AccessControlType);
            //        Console.WriteLine(item.FileSystemRights.ToString());
            //        Console.WriteLine(item.InheritanceFlags);
            //        Console.WriteLine(item.IdentityReference);

            //    }
            //}

            
  
            Console.ReadKey();
        }
    }
}
