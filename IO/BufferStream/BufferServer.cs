using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BufferStream
{
    class BufferServer
    {
        public static void Start(IPAddress address, int port)
        {
            // This is a Windows Sockets 2 error code.
            const int WSAETIMEDOUT = 10060;

            Socket serverSocket;
            int bytesReceived, totalReceived = 0;
            byte[] receivedData = new byte[2_000_000];

            // Create random data to send to the client.
            byte[] dataToSend = new byte[2000000];
            new Random().NextBytes(dataToSend);
            IPEndPoint ipEndpoint = new IPEndPoint(address, port);

            // Create a socket and listen for incoming connections.
            using (Socket listenSocket = new Socket(
                AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp))
            {
                listenSocket.Bind(ipEndpoint);
                listenSocket.Listen(1);

                // Accept a connection and create a socket to handle it.
                serverSocket = listenSocket.Accept();
                Console.WriteLine("[Server]Server is connected.\n");
            }

            try
            {
                // Set the timeout for receiving data to 2 seconds.
                serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 20000);

                // Receive data from the client.
                Console.WriteLine("[Server]Receiving data ... ");
                try
                {
                    do
                    {
                        //bytesReceived = serverSocket.Receive(receivedData, 0, receivedData.Length, SocketFlags.None);
                        //bytesReceived = serverSocket.Receive(receivedData);
                        bytesReceived = serverSocket.Receive(receivedData, 0, 4096, SocketFlags.None);
                        totalReceived += bytesReceived;
                    }
                    while (bytesReceived != 0);
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode == WSAETIMEDOUT)
                    {
                        // Data was not received within the given time.
                        // Assume that the transmission has ended.
                    }
                    else
                    {
                        Console.WriteLine("{0}: {1}\n", e.GetType().Name, e.Message);
                    }
                }
                finally
                {
                    Console.WriteLine("{0} megabytes received.\n", totalReceived/1024/1024);
                }
            }
            finally
            {
                serverSocket.Shutdown(SocketShutdown.Both);
                Console.WriteLine("[Server]Connection shut down.");
                serverSocket.Close();
                Console.Write("[Server]over");
                Console.ReadLine();
            }
        }

    }
}
