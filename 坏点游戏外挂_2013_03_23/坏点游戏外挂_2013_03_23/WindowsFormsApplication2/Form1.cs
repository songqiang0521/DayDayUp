using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;


namespace WindowsFormsApplication2
{


    public partial class Form1 : Form
    {



        //Task task;

        Point pointLeftUp;
        Point pointRightDown;
        public Form1()
        {
            InitializeComponent();
        }


        Point point = new Point();
        //Point screenPoint = new Point();

        public void MoveCursorOverButton(Button button)
        {
            IntPtr handle = IntPtr.Zero;
            //Point point;
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            bool coordinatesFound = false;



            if (button != null)
            {
                width = button.Size.Width;
                height = button.Size.Height;
                handle = button.Handle;
            }

            if ((width > 0) && (height > 0))
            {
                point = new Point();
                x = (width / 2);
                y = (height / 2);

                coordinatesFound = NativeMethods.ClientToScreen(handle, ref point);

                if (coordinatesFound == true)
                {
                    NativeMethods.SetCursorPos(point.X + x, point.Y + y);
                }
            }
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            //MoveCursorOverButton(this.button1);
            //label1.Text = "ClientPoint:" + button1.Location.ToString();
            //label2.Text = "ScreeenPoint:" + point.ToString();

            Rectangle rect = new Rectangle();
            bool got = NativeMethods.GetWindowRect(this.Handle, out rect);
            if (got)
            {
                label1.Text = rect.ToString();
            }

            Point p1 = rect.Location;
            Point p2 = new Point(rect.Size);
            MessageBox.Show(p1.ToString() + p2.ToString());

            if (NativeMethods.ScreenToClient(this.Handle, ref p1))
            {
                label2.Text = p1.ToString();
            }

            if (NativeMethods.ScreenToClient(this.Handle, ref p2))
            {
                label3.Text = p2.ToString();
            }







        }
        Thread thread;
        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(Work);
            thread.Start();

        }

        public void Work()
        {
            while (true)
            {
                NativeMethods.GetCursorPos(out point);
                Invoke(new Action(() => { label4.Text = point.ToString(); }));
                Thread.Sleep(200);
                //label4.Text = point.ToString();
            }



        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            NativeMethods.GetCursorPos(out point);
            label3.Text = point.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }

        }



        private byte[] rgbValues;
        private void button1_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = Convert.ToInt32(textBox2.Text);
            Color color = GetPixelColor(x, y);
            listBox1.Items.Add(color.ToString());

            //var g = this.CreateGraphics();


            #region main

            pointLeftUp = new Point(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox5.Text));
            pointRightDown = new Point(Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox6.Text));

            int width = pointRightDown.X - pointLeftUp.X;
            int height = pointRightDown.Y - pointLeftUp.Y;


            Bitmap bitmap = new Bitmap(width, height);
            //下面这句话把graphics和bitmap联系在一起！！
            Graphics gSave = Graphics.FromImage(bitmap);

            gSave.CopyFromScreen(pointLeftUp, new Point(0, 0), new Size(width, height));
            bitmap.Save(Application.StartupPath + DateTime.Now.Year + ".png");


            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            IntPtr bmpPtr = bmpData.Scan0;
            int bytesCount = bmpData.Stride * bmpData.Height;
            if (rgbValues==null)
            {
                rgbValues = new byte[bytesCount];
            }
            //byte[] rgbValues = new byte[bytesCount];//1410000=4*750*470
            //from bmpPtr to rgbValues
            Marshal.Copy(bmpPtr, rgbValues, 0, bytesCount);

            //height=行数
            //width =列数
            byte originalByte = (byte)((rgbValues[0] + rgbValues[1] + rgbValues[2]) / 3);
            byte newByte = 0;
            for (int i = 0; i < height; i+=2)//rows 每次隔5pixel
            {
                for (int j = 0; j < width; j+=2)//
                {
                    //下面这行i*4 太bug了。一定要注意索引啊！！
                    newByte = (byte)((rgbValues[i * width * 4 + j * 4] + rgbValues[i * width * 4 + j * 4 + 1] + rgbValues[i * width * 4 + j * 4 + 2]) / 3);
                    if (originalByte != newByte)
                    {
                        int tempx = j;//列
                        int tempy = i;//行
                        Point result = new Point(pointLeftUp.X + tempx, pointLeftUp.Y + tempy);
                        Rectangle rect = new Rectangle(result, new Size(5, 5));
                        //ControlPaint.DrawReversibleFrame(rect, Color.Red, FrameStyle.Dashed);
                        ControlPaint.FillReversibleRectangle(rect, Color.Black);

                        Point buttonPoint;
                        NativeMethods.GetCursorPos(out buttonPoint);
                        NativeMethods.SetCursorPos(result.X, result.Y);
                        NativeMethods.mouse_event((int)(NativeMethods.MouseEventFlags.LEFTDOWN ), 0,0, 0, 0);
                        NativeMethods.mouse_event((int)NativeMethods.MouseEventFlags.LEFTUP, 0,0, 0, 0);
                        NativeMethods.SetCursorPos(buttonPoint.X, buttonPoint.Y);
                        return;
                    }
                }
            }

            #endregion







            //Graphics g = Graphics.FromHdc(this.Handle);
            ////g.ClipBounds.Width
            ////Image image = new Bitmap(Application.StartupPath+"BadPoint.png");

            //Image image=new Bitmap((int)g.ClipBounds.Width,(int)g.ClipBounds.Height);

            //g.CopyFromScreen(100, 517, 0, 0, new Size(762, 474));

            //g.DrawImage(image, 0, 0);
            //image.Save(Application.StartupPath+"BadPoint.png");

        }

        private Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
            uint pixel = NativeMethods.GetPixel(hdc, x, y);
            NativeMethods.ReleaseDC(IntPtr.Zero, hdc);

            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                      (int)(pixel & 0x0000FF00) >> 8,
                      (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pointLeftUp = new Point(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox5.Text));
            pointRightDown = new Point(Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox6.Text));

            int width = pointRightDown.X - pointLeftUp.X;
            int height = pointRightDown.Y - pointLeftUp.Y;

            //int hSteps = width / 20;
            //int vSteps = height / 20;

            IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
            uint pixel = NativeMethods.GetPixel(hdc, pointLeftUp.X + 2, pointLeftUp.Y + 2);
            Color orignalColor = Color.FromArgb((int)(pixel & 0x000000FF),
                      (int)(pixel & 0x0000FF00) >> 8,
                      (int)(pixel & 0x00FF0000) >> 16);



            //Color orignalColor=GetPixelColor(pointLeftUp.X + 2, pointLeftUp.Y + 2);

            Color newColor;
            for (int i = 0; i < width; i += 10)
            {
                for (int j = 0; j < height; j += 10)
                {
                    int tempx = pointLeftUp.X + i;
                    int tempy = pointLeftUp.Y + j;

                    //IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
                    uint pixel2 = NativeMethods.GetPixel(hdc, tempx, tempy);

                    newColor = Color.FromArgb((int)(pixel2 & 0x000000FF),
                      (int)(pixel2 & 0x0000FF00) >> 8,
                      (int)(pixel2 & 0x00FF0000) >> 16);

                    //newColor= GetPixelColor(tempx, tempy);
                    if (!orignalColor.Equals(newColor))
                    {
                        //NativeMethods.SetCursorPos(tempx, tempy);
                        Rectangle rect = new Rectangle(tempx, tempy, 5, 5);
                        ControlPaint.DrawReversibleFrame(rect, Color.Red, FrameStyle.Thick);


                        NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
                        return;
                    }

                }

            }


        }
    }




    public class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);


        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

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
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        //[DllImport("user32.dll")]
        //public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
    }
}
