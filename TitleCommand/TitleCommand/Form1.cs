using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TitleCommand
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //获取"设置"文本的位置
            int captionHeight = SystemInformation.CaptionHeight;
            m_rect.X = this.Bounds.Width - 200;
            m_rect.Y = 5;
            m_rect.Width = 40;
            m_rect.Height = captionHeight;
        }
        Rectangle m_rect = new Rectangle(205, 6, 20, 20);
        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            int captionHeight = SystemInformation.CaptionHeight;
            m_rect.X = this.Bounds.Width - 200;
            m_rect.Y = 5;
            m_rect.Width = 40;
            m_rect.Height = captionHeight;
        }

        [DllImport("User32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("Kernel32.dll")]
        private static extern int GetLastError();
        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case 0x86://WM_NCACTIVATE
                    goto case 0x85;

                case 0x85://WM_NCPAINT
                    {
                        IntPtr hDC = GetWindowDC(m.HWnd);
                        //把DC转换为.NET的Graphics就可以很方便地使用Framework提供的绘图功能了
                        Graphics gs = Graphics.FromHdc(hDC);
                        //gs.FillRectangle(new LinearGradientBrush(m_rect, Color.Pink, Color.Purple, LinearGradientMode.BackwardDiagonal), m_rect);
                        StringFormat strFmt = new StringFormat();
                        strFmt.Alignment = StringAlignment.Center;
                        strFmt.LineAlignment = StringAlignment.Center;
                        strFmt.FormatFlags |= StringFormatFlags.NoWrap;
                        
                        gs.DrawString("设置", this.Font, Brushes.Black, m_rect, strFmt);
                        gs.Dispose();
                        //释放GDI资源
                        ReleaseDC(m.HWnd, hDC);
                        break;
                    }

                case 0xA1://WM_NCLBUTTONDOWN
                    {
                        Point mousePoint = new Point((int)m.LParam);
                        mousePoint.Offset(-this.Left, -this.Top);
                        if (m_rect.Contains(mousePoint))
                        {
                            MessageBox.Show("Hello");

                        }
                        break;
                    }
            }

        }

    }
}
