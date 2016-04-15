http://www.codeproject.com/Articles/13382/A-simple-application-using-I-O-Completion-Ports-an

==Introduction

The primary objective of this code submission is to provide source code which will demonstrate the use of IOCP using WinSock. This code submission tries to highlight the use of IOCP using a very easy to understand source code: the client and the server perform very simple operations; basically, they send and receive simple string messages. The IOCP client will allow you to stress test the server. You can send a message and provide input on how many times the message should be sent to the server.

Additionally, I have included socket1.zip, this code submission contains a one-to-one client and server implementation, and doesn't use IOCP. Similarly, socket2.zip contains a multi-threaded socket based server that creates a thread for every client connection, and is not a good design. I have included these additional codes so that the reader can compare these implementations with the IOCP implementation. This will provide additional insights to the understanding of the use of IOCP using WinSock.

==Background

I was considering the use of IOCP in my current project. I found IOCP using WinSock to be a very useful, robust, and scalable mechanism. IOCP allows an application to use a pool of threads that are created to process asynchronous I/O requests. This prevents the application from creating one thread per client which can have severe performance issues (socket2.zip contains a one thread per client implementation).

==Using the code

I have provided four zip files:
•Socket1.zip contains a simple one-to-one client and server implementation using WinSock. This is a pretty straightforward implementation; I won't be discussing this code. 
•Socket2.zip contains a multi-threaded server, one thread per client; the client code remains the same and is not discussed. 
•ServerIOCP.zip contains a multi-threaded server that uses IOCP, and is the focus of this article. 
•ClientIOCP.zip contains the IOCP client; the IOCP client can stress-test the IOCP server. 
•IOCPExecutables.zip contains the IOCP client and server executables. 

All of the code submissions are console based applications.
