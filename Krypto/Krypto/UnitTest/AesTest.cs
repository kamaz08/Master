using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using L3.Model;

namespace UnitTest
{
    [TestClass]
    public class AesTest
    {
        private readonly byte[] _Key = new byte[] { 211, 187, 84, 236, 22, 13, 160, 146, 224, 14, 23, 44, 173, 23, 22, 104, 98, 91, 31, 248, 59, 149, 145, 184, 157, 135, 203, 37, 224, 121, 108, 106 };
        private readonly byte[] _Iv = new byte[] { 53, 102, 191, 180, 187, 168, 75, 158, 61, 236, 136, 9, 123, 253, 251, 34 };

        string _test = @"testowy ciag znakow óżćłą;]'\[;][123;1";

        [TestMethod]
        public void CBC()
        {
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.CBC);
            var x = aes.Encrypt(_test, new KeyIvModel { Key = _Key, IV = _Iv });

            var result = aes.Decrypt(x, new KeyIvModel { Key = _Key, IV = _Iv });
            Assert.AreEqual(result, _test);
        }

        [TestMethod]
        public void CFB()
        {
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.CFB);
            var x = aes.Encrypt(_test, new KeyIvModel { Key = _Key, IV = _Iv });

            var result = aes.Decrypt(x, new KeyIvModel { Key = _Key, IV = _Iv });
            Assert.AreEqual(result, _test);
        }

        [TestMethod]
        public void CTS()
        {
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.CTS);
            var x = aes.Encrypt(_test, new KeyIvModel { Key = _Key, IV = _Iv });

            var result = aes.Decrypt(x, new KeyIvModel { Key = _Key, IV = _Iv });
            Assert.AreEqual(result, _test);
        }

        [TestMethod]
        public void ECB()
        {
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.ECB);
            var x = aes.Encrypt(_test, new KeyIvModel { Key = _Key, IV = _Iv });

            var result = aes.Decrypt(x, new KeyIvModel { Key = _Key, IV = _Iv });
            Assert.AreEqual(result, _test);
        }

        [TestMethod]
        public void OFB()
        {
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.OFB);
            var x = aes.Encrypt(_test, new KeyIvModel { Key = _Key, IV = _Iv });

            var result = aes.Decrypt(x, new KeyIvModel { Key = _Key, IV = _Iv });
            Assert.AreEqual(result, _test);
        }
    }
}
