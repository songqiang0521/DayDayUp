using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace BufferClient
{



    class BufferClinet
    {
        const int dataArraySize = 5;
        const int streamBufferSize = 1000;
        const int numberOfLoops = 100000;

        static void Main(string[] args)
        {
            //if (args.Length==0)
            //{
            //    Console.WriteLine("参数错误");
            //    return;
            //}

           

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //如何异步？？？
            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 5000));
            Console.WriteLine("连接服务器成功");


           Stream netStream=new NetworkStream(clientSocket,true);
           
               //给netStream加一个BufferedStream，并指定缓冲区大小streamBufferSize
           Stream bufStream = new BufferedStream(netStream, streamBufferSize);
               
                   Console.WriteLine("网络流 {0} 查找.\n",bufStream.CanSeek ? "支持" : "不支持");
                   if (bufStream.CanWrite)
                   {
                       //对直接用网络流发送数据和
                       //用缓冲封装后发送数据对比
                       SendData(netStream,bufStream);
                   }
                   if (bufStream.CanRead)
                   {
                       RecieveData(netStream, bufStream);
                   }
                   //netStream.Dispose();
                   //bufStream.Dispose();
               
           
            Console.WriteLine("结束");
            Console.ReadLine();
        }

        private static void RecieveData(Stream netStream, Stream bufStream)
        {
            DateTime startTime;
            double networkTime, bufferedTime = 0;
            int bytesReceived = 0;
            byte[] receivedData = new byte[dataArraySize];

            // Receive data using the NetworkStream.
            Console.WriteLine("Receiving data using NetworkStream.");
            startTime = DateTime.Now;
            while (bytesReceived < numberOfLoops * receivedData.Length)
            {
                //直接从网络流中读取数据
                //一次仅仅读取receivedData.Length(很小)个数据
                bytesReceived += netStream.Read(
                    receivedData, 0, receivedData.Length);
            }
            networkTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes received in {1} seconds.\n",
                bytesReceived.ToString(),
                networkTime.ToString("F1"));




            // Receive data using the BufferedStream.
            Console.WriteLine("Receiving data using BufferedStream.");
            bytesReceived = 0;
            startTime = DateTime.Now;
            int numBytesToRead = receivedData.Length;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.

                int n = bufStream.Read(receivedData, 0, receivedData.Length);
                // The end of the file is reached.

                if (n == 0)
                    break;
                bytesReceived += n;
                numBytesToRead -= n;
            }

            bufferedTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes received in {1} seconds.\n",
                bytesReceived.ToString(),
                bufferedTime.ToString("F1"));

            // Print the ratio of read times.

            Console.WriteLine("Receiving data using the buffered network" +
                " stream was {0} {1} than using the network stream alone.",
                (networkTime / bufferedTime).ToString("P0"),
                bufferedTime < networkTime ? "faster" : "slower");

        
        }

        private static void SendData(Stream netStream, Stream bufStream)
        {
            DateTime startTime;
            double networkTime, bufferedTime;
            //要发送的数据
            byte[] dataToSend = new byte[dataArraySize];
            new Random().NextBytes(dataToSend);

            //使用NetworkStream发送数据
            Console.WriteLine("Sending data using NetworkStream.");
            startTime = DateTime.Now;

            for (int i = 0; i < numberOfLoops; i++)
            {
                //对网络流直接调用10000次，性能不高
                netStream.Write(dataToSend, 0, dataToSend.Length);
            }
            netStream.Flush();
            networkTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes sent in {1} seconds.\n", numberOfLoops * dataToSend.Length,
                networkTime.ToString("F1"));


            //使用BufferedStream发送数据
            Console.WriteLine("Sending data using BufferedStream.");
            startTime = DateTime.Now;
            for (int i = 0; i < numberOfLoops; i++)
            {
                //当缓冲流到达指定大小时，自己调用网络流发送数据
                bufStream.Write(dataToSend, 0, dataToSend.Length);
            }
            bufStream.Flush();
            bufferedTime = (DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("{0} bytes sent in {1} seconds.\n",
                numberOfLoops * dataToSend.Length,
                bufferedTime.ToString("F1"));


            //速度比较
            Console.WriteLine("Sending data using the buffered " +
              "network stream was {0} {1} than using the network " +
              "stream alone.\n",
              (networkTime / bufferedTime).ToString("P0"),
              bufferedTime < networkTime ? "faster" : "slower");

       
        }
    }
}
