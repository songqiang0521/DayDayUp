using System;
using System.Threading;

namespace Telnet.Demo
{
    /// <summary>
    /// Demo for the telnet class:
    /// <p>
    /// <a href="http://www.klausbasan.de/misc/telnet/index.html">Further details</a>
    /// </p>
    /// </summary>
    public class TerminalDemo
    {
        /// <summary>
        /// The main entry point for the application.
        /// Can be used to test the programm and run it from command line.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
  
            //POP3_163();
            SMTP_163();
        }

        private static void SMTP_163()
        {


            //
            //c3ExOTg5NTIxNjI0QDE2My5jb20=
            //
            //U1ElQCFfMTYz
            //

            Lwolf.Telnet tn = new Lwolf.Telnet("SMTP.163.com", 25, 100);
            bool connected = tn.Connect();
            int suc = tn.WaitFor("220");

            tn.Send("helo 163");
            suc = tn.WaitFor("250 OK");

            tn.Send("auth login");
            suc = tn.WaitFor("334");


            tn.Send("c3ExOTg5NTIxNjI0QDE2My5jb20=");
            suc = tn.WaitFor("334");

            tn.Send("U1ElQCFfMTYz");
            suc = tn.WaitFor("235");

            tn.Send("mail from:<sq1989521624@163.com>");
            suc = tn.WaitFor("250");

            tn.Send("rcpt to:<814905275@qq.com>");//��ʵ�ռ��˵ĵ�ַ
            suc = tn.WaitFor("250");

            tn.Send("data");
            suc = tn.WaitFor("354");

            tn.Send("from:sq1989521624@163.com");//��ʾ�ķ����˵ĵ�ַ,���������ԣ����������ʵ�ʵķ����˵�ַ��һ�����Ͳ��ɹ�
            tn.Send("to:songqiang0521@163.com");//��ʾ���ռ��˵ĵ�ַ,�����ԣ�����������д
            tn.Send("subject:hello from tlenet");
            tn.Send("");

            tn.Send("this is a mail from telnet");

            tn.Send("\r\n.");



            //tn.WaitFor("554");
            Console.WriteLine(tn.SessionLog);









        }

        private static void POP3_163()
        {


            Lwolf.Telnet tn = new Lwolf.Telnet("POP3.163.com", 110, 20);
            bool connected = tn.Connect();
            int suc = tn.WaitFor("welcome");
            tn.Send("user sq1989521624@163.com");
            suc = tn.WaitFor("OK");
            tn.Send("pass SQ%@!_163");

            suc = tn.WaitFor("OK");

            tn.Send("list");
            tn.WaitFor("\r\n.");
            tn.Send("retr 1");
            tn.WaitFor("\r\n\r\n");

            Console.WriteLine(tn.SessionLog);


        }

       
    } // class
} // namespace