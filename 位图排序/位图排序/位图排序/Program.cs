using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 位图排序
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] inputArray = new int[] {3,2,4,6,33,4,3,5,2,7,5,77,2,3,5,4,2,5,1,2,5,7 };
            int[] outputArray = new int[4];

           outputArray= ConvertToBits(inputArray, outputArray);
            PrintBits(outputArray);
           





            Console.ReadKey();


        }

        private static void PrintBits(int[] outputArray)
        {
            for (int i = 0; i < outputArray.Length; i++)
            {
                if (outputArray[i] < 1)
                {
                    continue;
                }
                else
                {
                    while (outputArray[i]-->0)
                    {
                        Console.Write(i.ToString().PadLeft(2));
                    }
                }
                Console.WriteLine();
            }








        }

        private static int[] ConvertToBits(int[] inputArray, int[] outputArray)
        {
            for (int i = 0; i < inputArray.Length; i++)
            {

                outputArray= EnsureCapacity(outputArray, inputArray[i]);
                if (outputArray[inputArray[i]] == 0)
                {
                    outputArray[inputArray[i]] = 1;
                }
                else
                {
                    outputArray[inputArray[i]]++;

                }
                

            }
            return outputArray;
        }
        /// <summary>
        /// 确保数组不会越界
        /// </summary>
        /// <param name="inputArray">数组要确保的</param>
        /// <param name="index">试探索引</param>
        private static int[] EnsureCapacity(int[] inputArray, int index)
        {
            int inputLenth = inputArray.Length;
            if (index<=inputLenth-1)
            {
                return inputArray;
            }
            int[] newArray = new int[index + 1];
            Array.Copy(inputArray, newArray, inputLenth);

            inputArray = newArray;
            return inputArray;

        }
    }
}
