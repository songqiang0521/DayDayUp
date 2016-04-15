using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace SendMessage
{
    public partial class Form1 : Form
    {

        int x = 0;
        int y = 0;
        public Form1()
        {
            InitializeComponent();
             x=this.ClientSize.Width - 50;
            y=this.ClientSize.Height - 50;
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(EnumWindowsProc lpwndproc, IntPtr lParam);


        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);





        public bool EnumWindowsProcess(IntPtr hwnd, IntPtr lparam)
        {
            StringBuilder strbTitle = new StringBuilder();
            int nLength = GetWindowText(hwnd, strbTitle, strbTitle.Capacity + 1);
            this.listBox1.Items.Add(hwnd.ToString("X")+"||"+strbTitle.ToString());


            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            const uint BM_CLICK = 0xF5;

            IntPtr hWnd = NativeMethods.FindWindow(null, @"无标题 - 记事本");

            NativeMethods.ShowWindow(hWnd, NativeMethods.WindowStates.SW_SHOWMAXIMIZED);
            //NativeMethods.SendMessageW(hWnd, (uint)0x0100, (uint)0x41, 10);
            //NativeMethods.SetForegroundWindow(hWnd);
            //NativeMethods.SendMessageW(hWnd, (uint)0x0102, (uint)0x41, 10);
            //NativeMethods.SetWindowText(hWnd, "aiaiai");
            //NativeMethods.SendMessage(hWnd, 0x000c, IntPtr.Zero, "AAAAA");
            var childWnd = NativeMethods.FindWindowEx(hWnd, IntPtr.Zero, "Edit", null);
            NativeMethods.SendMessage(childWnd, 0x0102, 0x41, 10);


            IntPtr formWnd = NativeMethods.FindWindow(null, "Form1");
            IntPtr buttonWnd = NativeMethods.FindWindowEx(formWnd, IntPtr.Zero, null, "button2");



            NativeMethods.SendMessage(button2.Handle, BM_CLICK, 0, 0);


            NativeMethods.SendMessage(buttonWnd, BM_CLICK, 0, 0);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //启动notepad.exe 记事本程序，并在d:\下创建 或 打开 text_test.txt文件
            System.Diagnostics.Process txt = System.Diagnostics.Process.Start(@"notepad.exe", @"d:\text_test.txt");
            txt.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //等待一秒，以便目标程序notepad.exe输入状态就绪
            txt.WaitForInputIdle(1000);
            //如果目标程序 notepad.exe 没有停止响应，则继续
            if (txt.Responding)
            {

                //开始写入内容
                SendKeys.SendWait("-----下面的内容是外部程序自动写入-----\r\n");

                SendKeys.SendWait("heihei");     //将文本框内的内容写入
                SendKeys.SendWait("{Enter}{Enter}");     //写入2个回车

                SendKeys.SendWait("文档创建时间：");
                SendKeys.SendWait("{F5}");          //发送F5按键
                SendKeys.SendWait("{Enter}");       //发送回车键
             


                //NativeMethods.SetForegroundWindow(txt.Handle);
                //StringBuilder sb = new StringBuilder();


                //NativeMethods.GetWindowText(txt.Handle, sb, 256);

                for (int i = 0; i < 10; i++)
                {
                    Win32API.KeyDown(Keys.F5);
                    Win32API.KeyUp(Keys.F5);
                    Win32API.KeyDown(Keys.Enter);
                    Win32API.KeyUp(Keys.Enter);
                    //Thread.Sleep(1);
                    //Win32API.KeyUp(Keys.X);
                }

                //Win32API.KeyDown(Keys.Alt);
                //Win32API.KeyDown(Keys.F4);

                Win32API.KeyDown(Keys.Alt);
                //Win32API.KeyDown(Keys.F4);
                Win32API.KeyUp(Keys.F4);
                //Win32API.KeyUp(Keys.Alt);

                //Win32API.SendTwoKeyDown(Keys.Alt, Keys.F4);

                //SendKeys.SendWait("^s");       //发送 Ctrl + s 键
                //SendKeys.SendWait("%{F4}");      // 发送 Alt + F4 键
                

                //MessageBox.Show("文件已经保存成功！");
            }

        }


        [StructLayout(LayoutKind.Sequential)]
        public struct HWND__
        {

            /// int
            public int unused;
        }





        public static class NativeMethods
        {


            //Func<IntPtr, IntPtr, bool> EnumWindowsProc;







            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);



            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);


            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool SetWindowText(IntPtr hwnd, String lpString);


            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", SetLastError = true)]
            static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow(IntPtr hWnd);



            /// Return Type: LRESULT->LONG_PTR->int
            ///hWnd: HWND->HWND__*
            ///Msg: UINT->unsigned int
            ///wParam: WPARAM->UINT_PTR->unsigned int
            ///lParam: LPARAM->LONG_PTR->int
            [DllImport("user32.dll", EntryPoint = "SendMessageW")]
            [return: MarshalAs(UnmanagedType.I4)]
            public static extern int SendMessageW
                (
                [In()] IntPtr hWnd,
                uint Msg,
                 uint wParam,
                int lParam
                );


            public enum WindowStates
            {


                SW_SHOWNORMAL = 1,


                SW_SHOWMINIMIZED = 2,


                SW_SHOWMAXIMIZED = 3,


                SW_SHOWNOACTIVATE = 4,


                SW_SHOW = 5,


                SW_SHOWMINNOACTIVE = 7,


                SW_SHOWNA = 8,


                SW_SHOWDEFAULT = 10,

            }


            [DllImport("user32.dll")]
            public static extern int ShowWindow(IntPtr hWnd, WindowStates nCmdShow);

            [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


            [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
            public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

            //[DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
            //[return: MarshalAs(UnmanagedType.SysInt)]
            //public static extern int SendMessageW([In()] IntPtr hWnd,
            //    uint Msg,
            //    [MarshalAsAttribute(UnmanagedType.SysUInt)] uint wParam,
            //    [MarshalAs(UnmanagedType.SysInt)] int lParam);

            //[DllImportAttribute("user32.dll")]
            //public static extern int SendMessage(
            //    IntPtr hwnd,
            //    int msg, 
            //    IntPtr wParam, 
            //    [MarshalAs(UnmanagedType.LPStr)] string lParam);





        }

        private void button3_Click(object sender, EventArgs e)
        {
            const uint BM_CLICK = 0xF5; //鼠标点击的消息，对于各种消息的数值，大家还是得去API手册
            IntPtr hwndCalc = NativeMethods.FindWindow(null, "计算器"); //查找计算器的句柄
            if (hwndCalc != IntPtr.Zero)
            {
                IntPtr hwndThree = NativeMethods.FindWindowEx(hwndCalc, IntPtr.Zero, null, "3"); //获取按钮3 的句柄

                int errorCode = Marshal.GetLastWin32Error();

                IntPtr hwndPlus = NativeMethods.FindWindowEx(hwndCalc, IntPtr.Zero, null, "+");  //获取按钮 + 的句柄
                IntPtr hwndTwo = NativeMethods.FindWindowEx(hwndCalc, IntPtr.Zero, null, "2");  //获取按钮2 的句柄
                IntPtr hwndEqual = NativeMethods.FindWindowEx(hwndCalc, IntPtr.Zero, null, "="); //获取按钮= 的句柄
                NativeMethods.SetForegroundWindow(hwndCalc);    //将计算器设为当前活动窗口
                System.Threading.Thread.Sleep(2000);   //暂停2秒让你看到效果
                NativeMethods.SendMessage(hwndThree, BM_CLICK, 0, 0);
                System.Threading.Thread.Sleep(2000);   //暂停2秒让你看到效果
                NativeMethods.SendMessage(hwndPlus, BM_CLICK, 0, 0);
                System.Threading.Thread.Sleep(2000);   //暂停2秒让你看到效果
                NativeMethods.SendMessage(hwndTwo, BM_CLICK, 0, 0);
                System.Threading.Thread.Sleep(2000);   //暂停2秒让你看到效果
                NativeMethods.SendMessage(hwndEqual, BM_CLICK, 0, 0);
                System.Threading.Thread.Sleep(2000);
                MessageBox.Show("你看到结果了吗？");
            }
            else
            {
                MessageBox.Show("没有启动 [计算器]");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1.EnumWindows(EnumWindowsProcess, IntPtr.Zero);
            listBox1.Items.Add(listBox1.Items.Count.ToString());
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Activated");

        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Deactivated");
        }

        
        
        
        public override bool PreProcessMessage(ref Message msg)
        {
            listBox1.Items.Add("PreProcessMessage");
            return base.PreProcessMessage(ref msg);

            
        }

        

        private const int WM_ACTIVATEAPP = 0x001C;
        private bool appActive = true;


        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages. 
            switch (m.Msg)
            {
                // The WM_ACTIVATEAPP message occurs when the application 
                // becomes the active application or becomes inactive. 
                case WM_ACTIVATEAPP:

                    // The WParam value identifies what is occurring.
                    appActive = (((int)m.WParam != 0));

                    // Invalidate to get new text painted. 
                    this.Invalidate();

                    break;
            }
            base.WndProc(ref m);
        }


       
        protected override void OnPaint(PaintEventArgs e)
        {
            // Paint a string in different styles depending on whether the 
            // application is active. 
            if (appActive)
            {
              
                e.Graphics.FillRectangle(Brushes.Black, x, y, 30, 30);
                e.Graphics.DrawString("Application is active", this.Font, SystemBrushes.ActiveCaptionText, x-200, y);
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.InactiveCaption, x, y, 30, 30);
                e.Graphics.DrawString("Application is Inactive", this.Font, SystemBrushes.ActiveCaptionText, x - 200, y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Foem_Load");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            listBox1.Items.Add("KeyDown");
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Items.Add("MouseClick");
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


    }
}
