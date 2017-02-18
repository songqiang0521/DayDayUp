using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace run_test
{
    public class TestRunner
    {
        public Assembly LoadAssembly(string filename)
        {
            return Assembly.LoadFile(filename);
        }


        public IEnumerable<Type> GetTestableTypes(Assembly asm)
        {
            return asm.GetTypes().Where(t => t.GetCustomAttributes(typeof(TestClassAttribute), false).Length == 1);
        }


        private IEnumerable<MethodInfo> GetTestableMethods(Type testFixture)
        {
            return testFixture.GetMethods().Where(method => method.GetCustomAttributes(typeof(TestMethodAttribute), false).Length == 1);
        }

        private IEnumerable<AttrType> GetMethodAttributes<AttrType>(MethodInfo method)
        {
            return method.GetCustomAttributes(typeof(AttrType), false).Cast<AttrType>();

        }

        private bool IsDecoratedWithExpectedException(MethodInfo method, Exception expectedException)
        {
            Type expectedExceptionType = expectedException.GetType();

            return GetMethodAttributes<ExpectedExceptionAttribute>(method).Where(attr => attr.ExceptionType == expectedExceptionType).Count() != 0;
        }


        public void RunTests(Type testFixture, Action<string> result)
        {
            IEnumerable<MethodInfo> testMethods = GetTestableMethods(testFixture);

            if (testMethods.Count() == 0)
            {
                return;
            }


            var instance = Activator.CreateInstance(testFixture);
            foreach (MethodInfo mi in testMethods)
            {
                bool pass = true;
                try
                {
                    mi.Invoke(instance, null);
                    pass = true;
                }
                catch (Exception ex)
                {
                    pass = IsDecoratedWithExpectedException(mi, ex);
                }
                finally
                {
                    result(testFixture.Name + "." + mi.Name + ":" + (pass ? "pass" : "fail"));
                }


            }



        }
    }

}
