using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace DeleteDirectory
{
    class Program
    {
        //
        //http://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true
        //
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Thread.Sleep(2);
            Directory.Delete(target_dir, false);
        }

        static void Main(string[] args)
        {
        }
    }
}
