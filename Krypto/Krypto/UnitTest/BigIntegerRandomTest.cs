using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObliviousTransfer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class BigIntegerRandomTest
    {
        [TestMethod]
        public void RandomTest()
        {
            var random = new BigIntegerRandomGenerator();

            for(int i =0; i<10000; i++)
            {
                var a = random.Generate(100);
                Assert.IsTrue(a > 1);
            }
        }

    }
}
