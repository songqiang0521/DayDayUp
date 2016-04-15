using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace 枚举
{
    class Program
    {
        static void Main(string[] args)
        {
            String file = Assembly.GetEntryAssembly().Location;
            FileAttributes attributes = File.GetAttributes(file);
            Console.WriteLine("Is  {0}  hidden? {1}", file, (attributes & FileAttributes.Hidden) != 0);
            Console.WriteLine("Is  {0}  hidden? {1}", file,attributes.HasFlag(FileAttributes.Hidden));












            Console.WriteLine(Enum.GetUnderlyingType(typeof(Color)));
            var sss = Enum.GetNames(typeof(Color)).ToArray();
            foreach (var item in sss)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(Enum.GetValues(typeof(Color)));
            Console.WriteLine(Color.Blue.ToString("X"));

            Color c;

            Enum.TryParse<Color>("6", out c);
            Console.WriteLine(c.ToString());

            string[] strs = { "sq", "de", "df" };
            var resu = string.Join("----", strs);

            File.SetAttributes(@"D:\Global.cs", FileAttributes.ReadOnly);

            string[] names = { "sq","ds"};
            //var names2 = {"sss","frfr" };
            object[] objs ={ 12,35,32.0,"dw",DateTime.Now};
            foreach (var item in objs)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("over!");
            Console.ReadLine();
        }
    }

    public enum Color : byte
    {
        White,
        Red,
        Blue,
        Green
    }


}
