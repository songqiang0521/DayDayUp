using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 数组排序性能研究
{
    class Program
    {
        private class Int32Comparer : IComparer<int>
        {
            #region IComparer<int> Members

            public int Compare(int x, int y)
            {
                return x - y;
            }

            #endregion


            static void SortWithCustomComparer(int[] array)
            {
                Array.Sort(array, new Int32Comparer());
            }

            static void SortWithDelegate(int[] array)
            {
                Array.Sort(array, (x, y) => x - y);
            }

            static void SortWithDefaultComparer(int[] array)
            {
                Array.Sort(array, Comparer<int>.Default);
            }


            static void SortWithLinq(int[] array)
            {
                var sorted =
                    (from i in array
                     orderby i
                     select i).ToList();
            }

            static T[] CloneArray<T>(T[] source)
            {
                var dest = new T[source.Length];
                Array.Copy(source, dest, source.Length);
                return dest;

            }


            static void ParaLianq(int[] array)
            {
                //var q = array.AsParallel().OrderBy(item => item).ToArray();
            }

            static void Main(string[] args)
            {
                #region
                //List<int> list = new List<int>();
                //list.ForEach((item) => item=item * 2);
                //CodeTimer.Time("Thread Sleep", 1, () => { Thread.Sleep(3000); });
                //CodeTimer.Time("Empty Method", 10000000, () => { list.Add(100); });

                //Console.WriteLine("\n\n!");
                ////Console.ReadLine();
                //int iteration = 100 * 1000;

                //string s = "";
                //CodeTimer.Time("String Concat", iteration, () => { s += "a"; });

                //StringBuilder sb = new StringBuilder();
                //CodeTimer.Time("StringBuilder", iteration, () => { sb.Append("a"); });
                #endregion




                var random = new Random(DateTime.Now.Millisecond);
                var array = Enumerable.Repeat(0, 1000 * 500).Select(item => random.Next()).ToArray();

                CodeTimer.Initialize();

                CodeTimer.Time("ParaLianq()", 100, () => { ParaLianq(array); });

                CodeTimer.Time("Array.Sort()", 100, () => { Array.Sort<int>(array); });

                CodeTimer.Time("SortWithDefaultComparer", 100,
                    () => SortWithDefaultComparer(CloneArray(array)));

                CodeTimer.Time("SortWithCustomComparer", 100,
                    () => SortWithCustomComparer(CloneArray(array)));

                CodeTimer.Time("SortWithDelegate", 100,
                    () => SortWithDelegate(CloneArray(array)));

                CodeTimer.Time("SortWithLinq", 100,
                    () => SortWithLinq(CloneArray(array)));

                Console.ReadLine();

                Console.WriteLine("\nOver!");
                Console.ReadLine();


            }


        }
    }
}
