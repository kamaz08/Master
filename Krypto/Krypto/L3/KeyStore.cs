using L3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace L3
{
    public class KeyStore
    {
        private string _password;
        private string _keyStore;
        private readonly byte[] _Key = new byte[] { 211, 187, 84, 236, 22, 13, 160, 146, 224, 14, 23, 44, 173, 23, 22, 104, 98, 91, 31, 248, 59, 149, 145, 184, 157, 135, 203, 37, 224, 121, 108, 106 };
        private readonly byte[] _Iv = new byte[] { 53, 102, 191, 180, 187, 168, 75, 158, 61, 236, 136, 9, 123, 253, 251, 34 };

        private List<KeyIvModel> _keys;

        public KeyStore() : this("siemanko", "keystore.xd") { }
        public KeyStore(string password, string keyStore)
        {
            _password = password;
            _keyStore = keyStore;

            if (!File.Exists(keyStore))
            {
                var jsonSerializer = new JavaScriptSerializer();
                var json = jsonSerializer.Serialize(GenerateKeyIvList());
                var aes = new AesKrypto();
                var file = aes.Encrypt(json, new KeyIvModel { Key = _Key, IV = _Iv });
                try
                {
                    using (StreamWriter sr = new StreamWriter(keyStore))
                        sr.Write(file);
                }catch(Exception e)
                {
                    throw e;
                }
            }
        }

        public bool LoadKeys(string password)
        {
            if (_password != password)
                return false;

            string file;
            using (StreamReader sr = new StreamReader(_keyStore))
                file = sr.ReadToEnd();
            AesKrypto aes = new AesKrypto();
            file = aes.Decrypt(file, new KeyIvModel { Key = _Key, IV = _Iv });

            var jsonserialize = new JavaScriptSerializer();
            _keys = jsonserialize.Deserialize<List<KeyIvModel>>(file);
            return true;
        }

        public KeyIvModel GetKey(int i)
        {
            return _keys[i];
        }

        private List<KeyIvModel> GenerateKeyIvList()
        {
            var result = new List<KeyIvModel>();
            try
            {
                using (Aes myAes = Aes.Create())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        myAes.GenerateIV();
                        myAes.GenerateKey();
                        result.Add(new KeyIvModel
                        {
                            Key = myAes.Key,
                            IV = myAes.IV
                        });
                    }
                }
            }catch(Exception e)
            {
                throw e;
            }
            return result;
        }


    }
}
