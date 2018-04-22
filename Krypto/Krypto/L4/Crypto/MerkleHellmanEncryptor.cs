using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace L4.Crypto
{
    public static class MerkleHellman
    {
        public static List<BigInteger> Encrypt(List<BigInteger> key, Byte[] plainText)
        {
            var size = plainText.Length * 8;
            var blockSize = key.Count();
            if (size == 0) return null;
            var blockNumber = (size - 1) / blockSize + 1;


            var bitPlainText = new BitArray(plainText);
            var result = new List<BigInteger>();
            for (int i = 0; i < blockNumber; i++)
            {
                BigInteger res = 0;
                for (int j = 0; j < blockSize && i * blockSize + j < bitPlainText.Count; j++)
                    res += bitPlainText[(i + 1) * blockSize - j - 1] ? key[j] : 0;
                result.Add(res);
            }
            return result;
        }

        public static Byte[] Decrypt(List<BigInteger> key, BigInteger r, BigInteger q, List<BigInteger> cipher)
        {
            var r1 = BigInteger.ModPow(r, q - 2, q);
            var cipher2 = cipher.Select(x => (x * r1) % q).ToList();
            var blocksize = key.Count();
            var key2 = key.Select(x => x).ToList();
            key2.Reverse();

            var result = new List<Byte>();

            cipher2.ForEach(c =>
            {
                int count = 0;
                Byte res = 0;
                Byte value = 1;
                key2.ForEach(k =>
                {

                    if (c >= k)
                    {
                        res += value;
                        c = c - k;
                    }
                        
                    count++;
                    value *= 2;
                    if(count == 8)
                    {
                        result.Add(res);
                        count = 0;
                        res = 0;
                        value = 1;
                    }
                });
                if (res != 0)
                    result.Add(res);
            });
            return result.ToArray();
        }
    }
}
