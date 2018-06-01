using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ObliviousTransfer.Implementation
{
    public class KeyModel
    {
        public byte[] K0 { get; set; }
        public byte[] K1 { get; set; }
    }


    public class OneOutOfN
    {
        private SHA256CryptoServiceProvider _sha;
        private List<byte[]> _data;
        private int _n;
        private int _l;

        private List<KeyModel> _keyList;
        private List<byte[]> _cipherList;



        public OneOutOfN(int n, List<byte[]> data) : this(n, data, new Random(), new SHA256CryptoServiceProvider()) { }



        public OneOutOfN(int n, List<byte[]> data, Random random, SHA256CryptoServiceProvider sha)
        {
            _sha = sha;
            _n = n;
            _data = data;
            if (n == 0) return;
            _l = (int)Math.Log(n, 2);

            _keyList = new List<KeyModel>();

            for (int i = 0; i < _l; i++)
            {
                _keyList.Add(new KeyModel
                {
                    K0 = new byte[32],
                    K1 = new byte[32]
                });
                random.NextBytes(_keyList[i].K0);
                random.NextBytes(_keyList[i].K1);
            }
        }

        public List<KeyModel> GetKeys()
        {
            return _keyList;
        }

        public List<byte[]> GetCipher()
        {
            _cipherList = new List<byte[]>();
            for(int i = 0; i<_data.Count; i++)
            {
                _cipherList.Add(CipherMessage(i, _data[i]));
            }

            return _cipherList; 
        }

        public byte[] CipherMessage(int messNumber, byte[] mess)
        {
            var result = new byte[32];

            for(int i = 0; i < _keyList.Count; i++)
            {
                var x = messNumber % 2;
                messNumber /= 2;
                var key = x == 0 ? _keyList[i].K0 : _keyList[i].K1;
                key = _sha.ComputeHash(key);
                result = xor(result, key);
            }
            result = xor(mess, result);
            return result;
        }

        public byte[] xor(byte[] key, byte[] data)
        {
            var result = new byte[key.Length];

            for (int i = 0; i < key.Length; i++)
            {
                result[i] = data.Length > i ? (byte)(data[i] ^ key[i]) : key[i];
            }
            return result;
        }

        public byte[] sha(byte[] data)
        {
            return _sha.ComputeHash(data);
        }

    }
}
