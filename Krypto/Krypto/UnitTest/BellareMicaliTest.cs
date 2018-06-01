
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObliviousTransfer.Implementation;
using ObliviousTransfer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class BellareMicaliTest
    {
        [TestMethod]
        public void Test1()
        {
            var elGamal = new ElGamal();

            var priv = new BellareMicali();
            var pub = new BellareMicali();

            priv.ElGamalModel = elGamal.GenerateElGamalModel();
            pub.ElGamalModel = new ElGamalModel
            {
                P = priv.ElGamalModel.P,
                G = priv.ElGamalModel.G
            };

            pub.C = priv.C = elGamal.GenerateC(priv.ElGamalModel);

            pub.IsZero = true;

            pub.K = elGamal.GenerateC(pub.ElGamalModel);
            var keys = pub.GenerateKeyPairPublic();

            Assert.IsTrue(priv.CheckKeys(keys));

            var data = new List<byte[]> {
                new byte[] { 12,32,123,33 },
                new byte[] { 32, 188, 255, 0}
            };

            var cipher = priv.EncryptData(keys, data, new BigIntegerRandomGenerator());

            var result = pub.DecryptData(cipher);

            for (int i = 0; i < data[0].Length; i++)
                Assert.AreEqual(data[0][i], result[i]);
        }

        [TestMethod]
        public void Test2()
        {
            var elGamal = new ElGamal();

            var priv = new BellareMicali();
            var pub = new BellareMicali();

            priv.ElGamalModel = elGamal.GenerateElGamalModel();
            pub.ElGamalModel = new ElGamalModel
            {
                P = priv.ElGamalModel.P,
                G = priv.ElGamalModel.G
            };

            pub.C = priv.C = elGamal.GenerateC(priv.ElGamalModel);

            pub.IsZero = false;

            pub.K = elGamal.GenerateC(pub.ElGamalModel);
            var keys = pub.GenerateKeyPairPublic();

            Assert.IsTrue(priv.CheckKeys(keys));

            var data = new List<byte[]> {
                new byte[] { 12,32,123,33 },
                new byte[] { 32, 188, 255, 0}
            };

            var cipher = priv.EncryptData(keys, data, new BigIntegerRandomGenerator());

            var result = pub.DecryptData(cipher);

            for (int i = 0; i < data[1].Length; i++)
                Assert.AreEqual(data[1][i], result[i]);
        }
    }
}
