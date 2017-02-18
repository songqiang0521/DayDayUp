using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tobetest
{
    [TestClass]
    public class Class1
    {

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadFile()
        {
            //File.OpenRead(Path.GetTempFileName())

            throw new ArgumentException();

        }



        [TestMethod]
        public void AddToNumbers()
        {
            int a = 0;
            int b = 1;
            Assert.IsTrue(a + b == 1);

        }


    }
}
