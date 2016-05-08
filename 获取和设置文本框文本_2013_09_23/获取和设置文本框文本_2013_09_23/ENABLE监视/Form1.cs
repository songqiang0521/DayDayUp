using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ENABLE监视
{
    public partial class Form1 : Form
    {

        Point currentPointInScreen;
        Thread thread;
        IntPtr hMainWndUnderCurs;
        IntPtr hWndWatcher;
        StringBuilder sb = new StringBuilder(256);





        public Form1()
        {
            InitializeComponent();
            buttonAction.Enabled = false;
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {











        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread.ThreadState !=ThreadState.Stopped)
            {
                thread.Abort();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(Work);
            thread.Start();
        }





        public void Work()
        {
           

            while (true)
            {
                NativeMethods.GetCursorPos(out currentPointInScreen);
                hMainWndUnderCurs = NativeMethods.WindowFromPoint(currentPointInScreen);
                Invoke(new Action(() => { this.textBox1.Text = hMainWndUnderCurs.ToString("x"); }));
                

                IntPtr hWnd = NativeMethods.FindWindow(null, @"无标题 - 记事本");
                var childWnd = NativeMethods.FindWindowEx(hWnd, IntPtr.Zero, "Edit", null);

                
                //获取edit控件中的文本
                NativeMethods.SendMessage(childWnd, 0x000D, 256, sb);


                if (sb.ToString()=="AA")
                {
                    NativeMethods.SetForegroundWindow(hWnd);
                    SendKeys.SendWait("出现了AA");
                }

                //sb = new StringBuilder(sb.ToString()+sb.ToString()+"!!!");
                //设置文本
                //NativeMethods.SendMessage(childWnd, 0x000C, 256, sb);

                //Invoke(new Action(() => { this.textBox1.Text = sb.ToString(); }));


                Invoke(new Action(() =>
                {
                    
                    textBox3.Text = currentPointInScreen.X.ToString();

                    textBox4.Text = currentPointInScreen.Y.ToString();

                }));


                Thread.Sleep(100);
            }



        }
    }


    public class NativeMethods
    {


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point Point);


        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "GetDlgItemTextA")]
        public  static extern int GetDlgItemText(IntPtr hDlg, int nIDDlgItem, [Out]StringBuilder lpString, int nMaxCount);


        /// <summary>
        /// 获取坐标处句柄
        /// </summary>
        /// <param name="hWndParent">主窗口句柄</param>
        /// <param name="Point">相对于主窗口句柄的客户坐标</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr RealChildWindowFromPoint(IntPtr hWndParent, Point Point);

        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, Point Point, int flag);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);


        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        const int MOUSEEVENTF_MOVE = 0x0001;        //移动鼠标
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;    //模拟鼠标左键按下
        public const int MOUSEEVENTF_LEFTUP = 0x0004;      //模拟鼠标左键抬起
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;   //模拟鼠标右键按下
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;     //模拟鼠标右键抬起
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;  //模拟鼠标中键按下
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;    //模拟鼠标中键抬起
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;    //标示是否采用绝对坐标 
        public static uint BM_CLICK = 0xF5;

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);



        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, StringBuilder lParam);


        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);


        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        //[DllImport("user32.dll")]
        //public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint next);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetCapture();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        //[DllImport("user32.dll")]
        //public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
    }


}
