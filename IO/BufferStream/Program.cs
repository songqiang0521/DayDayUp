using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace BufferStream
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskServer = Task.Run(() => { BufferServer.Start(IPAddress.Loopback, 5000); });

            //System.Threading.Thread.Sleep(2000);

            var taskClient = Task.Run(() => { BufferClinet.Start(IPAddress.Loopback, 5000); });



            Task.WaitAll(taskServer, taskClient);

            Console.WriteLine("over");





        }
    }
}
