using L4.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class MerkleHellmanTest
    {
        [TestMethod]
        public void GenerateTest()
        {
            var gen = new MerkleHellmanGenerator(10);
            var krypto = gen.Generate();

            BigInteger sum = 0;
            krypto.PrivateKey.ForEach(x =>
            {
                if (sum > x) Assert.Fail();
                sum += x;
            });
            if (krypto.Q < krypto.R) Assert.Fail();
            if (BigInteger.GreatestCommonDivisor(krypto.Q, krypto.R) > 1) Assert.Fail();


            for(int i =0; i< krypto.PrivateKey.Count(); i++)
            {
                Assert.AreEqual(krypto.PublicKey[i], krypto.PrivateKey[i] * krypto.R % krypto.Q);
            }
        }

        [TestMethod]
        public void EncryptTest()
        {
            var krypto = new CryptoModel
            {
                PrivateKey = new List<BigInteger> { 2, 7, 11, 21, 42, 89, 180, 354 },
                PublicKey = new List<BigInteger> { 295, 592, 301, 14, 28, 353, 120, 236 },
                BlockSize = 8,
                Q = 881,
                R = 588
            };

            var ciipher = MerkleHellman.Encrypt(krypto.PublicKey, new byte[] { 0b01100001 });
            Assert.AreEqual(ciipher[0], 1129);
        }

        [TestMethod]
        public void DecryptTest()
        {
            var krypto = new CryptoModel
            {
                PrivateKey = new List<BigInteger> { 2, 7, 11, 21, 42, 89, 180, 354 },
                PublicKey = new List<BigInteger> { 295, 592, 301, 14, 28, 353, 120, 236 },
                BlockSize = 8,
                Q = 881,
                R = 588
            };

            var plain = MerkleHellman.Decrypt(krypto.PrivateKey, krypto.R, krypto.Q, new List<BigInteger> { 1129 })[0];

            Assert.AreEqual(plain, 0b01100001);

        }
    }
}
