using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace 数组
{
    [DebuggerDisplay("SQ",Name="SQSQ",Target=typeof(Program))]
    class Program
    {
        [Flags]
        enum MyFlags
        {
            White=0x0001,
            Black=0x0002,
            Red=0x0004,
            Blue=0x0008

        }


        internal unsafe struct CharArray
        {
            /// <summary>
            /// 这个数组是内联在结构中的
            /// </summary>
            public fixed char Characters[2000];
        }

       const int ElementCount = 1000;

        
        private static  void  MyVoidNoneMethod1 ()
        {
            Console.WriteLine("Hello11111");
        }

        private static void MyVoidNoneMethod2()
        {
            Console.WriteLine("Hello22222");
        }
        private void MyInsMethod3()
        {
            
            Console.WriteLine("Hello33333");
        }
        static void Main(string[] args)
        {
            int? a = 4;
            int? b = 5;
            int? c = Convert.ToInt32( Convert.ToString(21,8));
            Console.WriteLine(a ?? b ?? c);
            Console.WriteLine(a.GetType().ToString());
            Console.WriteLine(c.GetType().ToString());


            ShowAttributes(typeof(Program));
            ShowAttributes(typeof(MyFlags));

            MyFlags flag = new MyFlags();
            flag = MyFlags.Black | MyFlags.Blue | MyFlags.Red;
            Console.WriteLine(flag.ToString());

            Console.WriteLine( RuntimeEnvironment.GetSystemVersion());

            Console.WriteLine(Environment.OSVersion.Version.Major);
            #region 
           // int[] source = { 1,2,3,4,5,6,7,8,9,10};
           // int[] dest=new int[source.Length];
           // Array.Copy(source, dest, 3);
           // foreach (var item in dest)
           // {
           //     Console.WriteLine(item);
           // }
           // Console.WriteLine("\n\n");
           // Array.ForEach(source, item => Console.WriteLine(item));
           //var modByThree= Array.FindAll(source, (item) => item % 3 == 0);
           //Array.ForEach(modByThree, item => Console.WriteLine(item));



           //int[][] jag ={
           //                 new[]{1,2,3},
           //                 new[]{3,4,6}
           //             };
           //Console.WriteLine(jag.ToString());
            #endregion


            #region

            //int testCount = 20;
            //Stopwatch sw;
            //// 二维数组
            //int[,] a2Dim = new int[ElementCount, ElementCount];

            ////二维的数组的竖着
            //int[][] aJagged=new int[ElementCount][];
            //for (int i = 0; i <aJagged.GetUpperBound(0); i++)
            //{
            //    aJagged[i]=new int[ElementCount];
            //}

            ////1、使用安全方法访问所有数组元素
            //sw = Stopwatch.StartNew();
            //for (int i = 0; i < testCount; i++)
            //{
            //    SafeDimArrayAccess(a2Dim);
            //}
            //Console.WriteLine("使用安全方法访问数组用时" + sw.Elapsed);

            ////2、使用交错数组访问所有数组元素
            //sw = Stopwatch.StartNew();
            //for (int i = 0; i < testCount; i++)
            //{
            //    SafeJaggedArrayAccess(aJagged);
            //}
            //Console.WriteLine("使用交错数组访问数组用时" + sw.Elapsed);


            ////3、使用不安全方法访问所有数组元素
            //sw = Stopwatch.StartNew();
            //for (int i = 0; i < testCount; i++)
            //{
            //    UnSafe2DimArrayAccess(a2Dim);
            //}
            //Console.WriteLine("使用不安全方法访问数组用时" + sw.Elapsed);

            #endregion


            #region
            //StackallocDemo();
            //InlineArrayDemo();
            #endregion

            #region
            Action action = null;
            action += new Action(Program.MyVoidNoneMethod1);
            action += new Action(Program.MyVoidNoneMethod2);
            action += Program.MyVoidNoneMethod2;
            action += new Action(new Program().MyInsMethod3);

            action.Invoke();
            var list= action.GetInvocationList();
            foreach (var item in list)
            {
                ((Action)item).Invoke();
                item.DynamicInvoke(null);
            }
            //Console.WriteLine(action.Target.ToString());
            Console.WriteLine(action.Method.Name);

            //action -= new Program().MyVoidNoneMethod1;
            //action -= new Action(new Program().MyVoidNoneMethod1);
            action = (Action)Delegate.Remove(action, new Action(Program.MyVoidNoneMethod1));
            action -= new Action(new Program().MyInsMethod3);
            action.Invoke();
            var list2 = action.GetInvocationList();
            foreach (var item in list2)
            {
                ((Action)item).Invoke();
                item.DynamicInvoke(null);
            }
            //Console.WriteLine(action.Target.ToString());
           // Console.WriteLine(action.Method.Name);
            #endregion





            Console.WriteLine("over");
            Console.ReadLine();
        }

        private static void ShowAttributes(MemberInfo attributeTarget)
        {

            Attribute[] attributes = Attribute.GetCustomAttributes(attributeTarget);
            Console.WriteLine("Attributes applied to {0}: {1}",
           attributeTarget.Name, (attributes.Length == 0 ? "None" : String.Empty));
            foreach (Attribute attribute in attributes)
            {
                // Display the  type of each applied attribute  
                Console.WriteLine("  {0}", attribute.GetType().ToString());
                DebuggerDisplayAttribute dda = attribute as DebuggerDisplayAttribute;
                if (dda != null)
                {
                    Console.WriteLine("    Value={0}, Name={1}, Target={2}",
                       dda.Value, dda.Name, dda.Target);
                }

                FlagsAttribute flags = attribute as FlagsAttribute;
                if (flags!=null)
                {
                    Console.WriteLine(flags.ToString());
                    Console.WriteLine(flags.GetType().Name);
                }


            }
        }

        private unsafe static void StackallocDemo()
        {
            const int Width = 1000000;
            char* pc = stackalloc char[Width];
            string s="Jeff Richer";
            for (int i = 0; i < Width; i++)
            {
                pc[Width - i - 1] = (i < s.Length) ? s[i] : '.';

            }

            Console.WriteLine(new string(pc, 0, Width));




        }

        private unsafe  static void InlineArrayDemo()
        {

            CharArray ca;
            int widthInBytes = sizeof(CharArray);
            int Width = widthInBytes / 2;
            string s = "Jeff Richer";
            for (int i = 0; i < Width; i++)
            {
                ca.Characters[Width - i - 1] = (i < s.Length) ? s[i] : '.';
            }
            Console.WriteLine(new string(ca.Characters,0,Width));















        }

        private unsafe static void UnSafe2DimArrayAccess(int[,] a2Dim)
        {
            int sum = 0;
            fixed (int* p = a2Dim)
            {
                for (int i = 0; i < a2Dim.GetUpperBound(0); i++)
                {
                    int baseofDim=i*a2Dim.GetUpperBound(0);

                    for (int j = 0; j < a2Dim.GetUpperBound(1); j++)
                    {
                        sum += p[baseofDim  + j];
                    }
                }
 
            }

            Console.WriteLine("\t" + sum);
        }

        private static void SafeJaggedArrayAccess(int[][] aJagged)
        {
            int sum = 0;
            for (int i = 0; i < aJagged.GetUpperBound(0); i++)
            {
                for (int j = 0; j < aJagged[0].GetUpperBound(0); j++)
                {
                    sum += aJagged[i][j];
                }
            }

            Console.WriteLine("\t"+sum);




        }

        private static void SafeDimArrayAccess(int[,] a2Dim)
        {
            int sum = 0;
            for (int i = 0; i < a2Dim.GetUpperBound(0); i++)
            {
                for (int j = 0; j < a2Dim.GetUpperBound(1); j++)
                {
                    sum+=a2Dim[i,j];
                }
            }

            Console.WriteLine("\t" + sum);




        }
    }
}
