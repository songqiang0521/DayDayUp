using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace 异常
{
    public static class StaticClass1
    {
        static StaticClass1()
        {
            Console.WriteLine("zhixingle");
        }


        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void M()
        {
            Console.WriteLine("static M()");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            AppDomain domain = AppDomain.CurrentDomain;
            domain.FirstChanceException += new EventHandler<System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs>(domain_FirstChanceException);

            //还没F10到这里，那些受保障的函数已经执行了。比如静态构造函数
            RuntimeHelpers.PrepareConstrainedRegions();
            

            try
            {
                Console.WriteLine("try block");
                //Environment.FailFast("我乐意");
                //throw new ArgumentException("sQ");


            }
            catch (ArgumentException e)
            {
                StaticClass1.M();
                Console.WriteLine(e.Message);

                //throw new ArgumentException("sq");
            }

            finally
            {
                //StaticClass1.M();
                Console.WriteLine("finally");
            }

            Console.WriteLine("other");

            Console.WriteLine("over");
            Console.ReadLine();

        }

        static void domain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
           Console.WriteLine( e.Exception.StackTrace);
            Console.WriteLine(sender.ToString());
            Console.WriteLine(e.Exception.ToString());
        }
    }
   

}
