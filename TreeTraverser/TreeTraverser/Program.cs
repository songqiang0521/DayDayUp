using System;
using System.IO;

namespace TreeTraverser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("usage:");
                Console.WriteLine("TreeTraverser %folder%");
                return;
            }

            string folderPath = args[0];
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("path invalid");
                return;
            }

            TreeTraverser tree = new TreeTraverser(folderPath, false, false);
            tree.Traverse();
            Console.Read();
        }
    }
}
