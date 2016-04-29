using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace multi
{
    class Program
    {
        static void Main(string[] args)
        {
            var ci = CultureInfo.CurrentCulture;
            //var ci = new CultureInfo("zh-CN");
            ResourceManager rm = new ResourceManager("multi.multi", Assembly.GetExecutingAssembly());
            string msg = rm.GetString("hello", ci);
            Console.WriteLine(msg);

            rm = new ResourceManager("multi.multi2", Assembly.GetExecutingAssembly());
            msg = rm.GetString("hello", ci);
            Console.WriteLine(msg);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Console.WriteLine(multi.hello);
            Console.WriteLine(multi2.hello);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Console.WriteLine(multi.hello);
            Console.WriteLine(multi2.hello);//界面的显示和UI相关
        }
    }
}
