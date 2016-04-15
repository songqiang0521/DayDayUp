using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 数组
{
    class Program
    {

        static void FillArray( int[] arr)
        {
            // Initialize the array:

            //arr = new int[5] { 1, 2, 3, 4, 5 };
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] += 10;
            }
        }

        static void Main(string[] args)
        {

            int[] ar5 = new int[5];
            FillArray(ar5);
            foreach (int item in ar5)
            {
                Console.WriteLine(item);
            }
            Cover(ar5);//输出后不改变ar5
            foreach (int item in ar5)
            {
                Console.WriteLine(item);
            }
            Cover(ref ar5);//改变了ar5

           
            foreach (int item in ar5)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
        /// <summary>
        /// 如果不用ref参数，那么这个函数推出后，ar5不会改变
        /// </summary>
        /// <param name="ar5"></param>
        private static void Cover(ref int[] ar5)
        {
           int[] newint=new int[10]{1,2,3,4,5,6,7,8,9,0};
           ar5 = newint;
        }
        private static void Cover( int[] ar5)
        {
            int[] newint = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            ar5 = newint;
        }
    }
}
