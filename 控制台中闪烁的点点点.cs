using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ConsoleApplication2
{
    class Program
    {
        static int index = 0;
        static int left=0;
        static string[] strings =
        {
            "start.",
            "start..",
            "start...",
            "start....",
            "start....."
        };
        static void Main(string[] args)
        {

            Console.Write("heihei......start......");
            left= Console.CursorLeft = "heihei......start......".IndexOf("start");


            Timer timer = new Timer(500);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            Console.ReadKey();

        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.CursorLeft = left;
            Console.Write("                  ");
            index++;
            Console.CursorLeft=left;
            Console.Write(strings[index%5]);



        }
    }
}
