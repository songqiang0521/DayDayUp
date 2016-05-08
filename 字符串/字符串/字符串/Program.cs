using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security;
using System.Runtime.InteropServices;

namespace 字符串
{
    /// <summary>
    /// 演示如何保存并显式安全字符串
    /// </summary>
    class Program
    {

        public static void Main()
        {
            using(SecureString ss=new SecureString())
            {
                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key==ConsoleKey.Enter)
                    {
                        break;
                    }
                    Console.Write("*");
                    ss.AppendChar(cki.KeyChar);
                }
                Console.WriteLine("\n input complete\n");
                Console.WriteLine(ss.ToString());
                
                
                
                Console.ReadLine();
                DisplayPassword(ss);



            }



            Console.Write("over");
            Console.ReadLine();


        }

        private unsafe static void DisplayPassword(SecureString ss)
        {
            char* pc = null;
            try
            {
                pc = (char*)Marshal.SecureStringToCoTaskMemUnicode(ss);
                while (*pc != 0)
                {
                    Console.Write(*pc++);
                }



            }
            finally
            {
                if (pc!=null)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)pc);
                }
            }


        }









//        static void Main(string[] args)
//        {
            
//            byte[] byy=new byte[1000];

//            new Random().NextBytes(byy);
//            Console.WriteLine(BitConverter.ToString(byy));
//            string base64 = Convert.ToBase64String(byy);
//            Console.WriteLine(base64);
//            byte[] res = Convert.FromBase64String(base64);
//            Console.WriteLine(BitConverter.ToString(res));



//            byte[] bytes1 = { 0x20, 0x23, 0xe2 };
//            byte[] bytes2 = { 0x98, 0xa3 };
//            char[] chars = new char[3];

//            Decoder d = Encoding.UTF8.GetDecoder();
//            int charLen = d.GetChars(bytes1, 0, bytes1.Length, chars, 0);
//            // The value of charLen should be 2 now.

//            charLen += d.GetChars(bytes2, 0, bytes2.Length, chars, charLen);
//            foreach (char c in chars)
//                Console.Write("U+{0:X4}  ", (ushort)c);




            
//            byte[] b2=Encoding.Default.GetBytes("我好喜欢你啊  哈哈");

//            Console.WriteLine(b2.Length);
//            byte[] encoded1= b2.Where((item, index) => index < 3).ToArray();
//            byte[] encoded2 = b2.Where((item, index) => index >=3).ToArray();
//            Console.WriteLine(BitConverter.ToString(encoded1));
//            Console.WriteLine(BitConverter.ToString(encoded2));

//            char[] chars1=new char[20];

//            Decoder decoder = Encoding.Default.GetDecoder();
//            int charLength= decoder.GetChars(encoded1, 0, encoded1.Length, chars1, 0);
//            int second=decoder.GetChars(encoded2, 0, encoded2.Length, chars1, charLength);
//            string ss = new string(chars1);
//            Console.WriteLine(ss);
//             Console.WriteLine("haha");
//            foreach (var item in chars1)
//            {
//                Console.WriteLine(item);
//            }

//            for (int i = 0; i < charLength+second; i++)
//            {
//                Console.WriteLine(chars1[i]);
//            }
//            Console.WriteLine("haha");
            


//           Console.WriteLine( Encoding.Default.GetString(encoded1));
//           Console.WriteLine(Encoding.Default.GetString(encoded2));





//            Console.WriteLine("\n\n\n\n");
//            Console.WriteLine(b2.ToString());
//            var query=b2.Select(item => item);
//            foreach (var item in query)
//            {
//                Console.Write(" " + item);
//            }
//            string result=BitConverter.ToString(b2);
//            Console.WriteLine(result);
//            string sq = @"sq nihao 哈哈 没有         
//            ☻嘿嘿o(∩_∩)o o(∩_∩)o 哈哈 ＋ 
//            ";
//            string sq2 = "SQ";
//            string sq3 = "宋";
//            int ii = 12345;
//           var iii= int.Parse("   123412", NumberStyles.AllowLeadingWhite);
//           Console.WriteLine(iii);
//            Console.WriteLine(ii.ToString("C",new CultureInfo("en-US")));

//            StringInfo info = new StringInfo(sq);
//            StringInfo info2 = new StringInfo(sq2);
//            StringInfo info3 = new StringInfo(sq3);

//            Console.WriteLine(info.LengthInTextElements.ToString());
//            Console.WriteLine(info2.LengthInTextElements.ToString());
//            Console.WriteLine(info3.LengthInTextElements.ToString());
//            Console.WriteLine("\n\n\n\n0");
//            Console.WriteLine(string.IsInterned("sq"));
//            Console.WriteLine(sq.ToUpper());
//            Console.WriteLine(sq.ToUpperInvariant());
//            sq.ToList().ForEach(
//                item=>
//                    {
//                        Console.WriteLine(item+"\t"+char.GetUnicodeCategory(item));

//                    }
//                );


//            Console.WriteLine("    "+Char.GetUnicodeCategory(' '));

//            /*
//            double d1 = 123.456d;
//            int i1 = 23;
//            Console.WriteLine("d1====={0,10:E}\ni1====={1}",d1,i1);
//            Console.WriteLine("d1====={0,10}\ni1====={1}", d1, i1);
//            Console.WriteLine("d1====={0,-10}\ni1====={1}", d1, i1);
//            Console.WriteLine("d1====={0:C}\ni1====={1}", d1, i1);
//            */

//            string Text = @"123,123，123，123";


//            //string Pattern = "中文";
//            //string Pattern = @"\bn";
//            //string Pattern = @"tion\b{1,}";
//            //string Pattern = @"\b[tc]\S*tion\b";
//            string Pattern = @"\，";
//            //string Pattern = @"(tion){1,}";



//            MatchCollection matchs = Regex.Matches(Text, Pattern,RegexOptions.IgnoreCase|RegexOptions.ExplicitCapture);

//            Console.WriteLine("共有"+matchs.Count+"个匹配项");
//            foreach (Match item in matchs)
//            {
//                Console.Write("位置"+item.Index+"      ");
//                if (item.Index < Text.Length - 10)
//                {
//                    Console.WriteLine(Text.Substring(item.Index, 5));
//                }
//                else
//                {
//                    Console.WriteLine("为了防止超出索引范围，不输出！！！");
//                }
                
//            }

          















//            /*
//            StringBuilder sb1 = new StringBuilder("SQ-guo-w-hellooooooo");
//            Console.WriteLine("转换前"+sb1);
//            Console.WriteLine("容量：{0}--大小:{1}",sb1.Capacity,sb1.Length);
//            sb1.Replace('o', 'O');
//            sb1 =sb1.Append( "sb");
//            Console.WriteLine("转换后"+sb1);
//            Console.WriteLine("容量：{0}--大小:{1}", sb1.Capacity, sb1.Length);
//            */
//            Console.ReadKey();
//        }
    }
}
