using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
   



    public class Binary<T>
    {

        /// <summary>
        /// 二分查找
        /// </summary>
        /// <param name="array">源数据</param>
        /// <param name="index">起始索引</param>
        /// <param name="length">数组长度</param>
        /// <param name="value">要查找的数据值</param>
        /// <param name="comparer">比较器（判断两数大小的规则）</param>
        /// <returns></returns>
        internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
        {
            int i = index;
            int num = index + length - 1;
            while (i <= num)
            {
                int num2 = i + (num - i >> 1);
                int num3 = comparer.Compare(array[num2], value);
                if (num3 == 0)
                {
                    return num2;
                }
                if (num3 < 0)
                {
                    i = num2 + 1;
                }
                else
                {
                    num = num2 - 1;
                }
            }
            return ~i;
        }
    }
    public partial class Form1 : Form
    {
        
        
        
//        [DllImport("kernel32", SetLastError = true)]
//public static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            int num = 10;
            int n = num + 6 >> 2;//加法比右移高

            this.Text = "Debug";

            int[] array = { 2,3,4,5,8,9,12,15,48,79,96};
           int result= Binary<int>.InternalBinarySearch(array, 5, array.Length, 48, Comparer<int>.Default);
           MessageBox.Show(result.ToString());



        }
    }
}
