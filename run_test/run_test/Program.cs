using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace run_test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRunner runner = new TestRunner();

            var asm = runner.LoadAssembly(args[0]);


            var types = runner.GetTestableTypes(asm);

            foreach (Type t in types)
            {
                runner.RunTests(t, a => Console.WriteLine(a));
            }




        }
    }
}
