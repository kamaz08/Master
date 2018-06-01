using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObliviousTransfer.Implementation;
using ObliviousTransfer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class OneOutOfNTest
    {
        private Random _random;
        private int _n;
        private List<byte[]> _data;

        [TestInitialize]
        public void SetUp()
        {
            _random = new Random();
            _n = 1024;
            _data = Enumerable.Range(0, _n).Select(x =>
            {
                var res = new byte[32];
                _random.NextBytes(res);
                return res;
            }).ToList();
            
        }


        [TestMethod]
        public void Test1()
        {
            for (int t = 0; t < _n; t++)
            {
                var test = new OneOutOfN(_n, _data);

                var cipher = test.GetCipher();

                var keyList = test.GetKeys();

                var messnumber = t;
                var mesTemp = messnumber;
                byte[] keys = new byte[32];
                keyList.ForEach(x =>
                {
                    bool isZero = mesTemp % 2 == 0;
                    mesTemp /= 2;
                    var key = OneOutOfTwo(x, isZero);

                    for (int i = 0; i < 32; i++)
                    {
                        Assert.IsTrue(key[i] == x.K0[i] || key[i] == x.K1[i]);
                    }


                    keys = test.xor(test.sha(key), keys);
                });

                var result = test.xor(cipher[messnumber], keys);

                for (int i = 0; i < result.Count(); i++)
                {
                    Assert.AreEqual(result[i], _data[messnumber][i]);
                }
            }
        }


        public byte[] OneOutOfTwo(KeyModel data, bool isZero)
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

            pub.IsZero = isZero;

            pub.K = elGamal.GenerateC(pub.ElGamalModel);
            var keys = pub.GenerateKeyPairPublic();

            Assert.IsTrue(priv.CheckKeys(keys));

            var cipher = priv.EncryptData(keys, new List<byte[]> {data.K0, data.K1 }, new BigIntegerRandomGenerator());

            var result = pub.DecryptData(cipher);

            return result;
        }




    }
}
