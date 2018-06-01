using System;
using System.Collections.Generic;
using System.Numerics;
using ObliviousTransfer.Model;
using System.Security.Cryptography;

namespace ObliviousTransfer.Implementation
{
    public class BellareMicali
    {
        private SHA256CryptoServiceProvider _sha;
        public ElGamalModel ElGamalModel { get; set; }
        public BigInteger C { get; set; }
        public BigInteger K { get; set; }
        public BigInteger T1 { get; set; }
        public BigInteger T2 { get; set; }
        public BigInteger T3 { get; set; }


        public bool IsZero { get; set; }
        public BellareMicali()
        {
            _sha = new SHA256CryptoServiceProvider();
        }

        public List<BigInteger> GenerateKeyPairPublic()
        {
            var know = BigInteger.ModPow(ElGamalModel.G, K, ElGamalModel.P);
            var dontknow = C * modInverse(know, ElGamalModel.P) % ElGamalModel.P;

            return IsZero ? new List<BigInteger> { know, dontknow } : new List<BigInteger> { dontknow, know };
        }

        public bool CheckKeys(List<BigInteger> list)
        {
            return list.Count == 2 && BigInteger.Multiply(list[0], list[1]) % ElGamalModel.P == C; 
        }

        public List<ElGamalData> EncryptData(List<BigInteger> keys, List<byte[]> data, IBigIntegerRandomGenerator gen)
        {
            var result = new List<ElGamalData> { new ElGamalData(), new ElGamalData() };

            var rand = gen.Generate(ElGamalModel.P.ToByteArray().Length - 2);

            result[0].Gx = BigInteger.ModPow(ElGamalModel.G, rand, ElGamalModel.P);
            T1 = BigInteger.ModPow(keys[0], rand, ElGamalModel.P);
            result[0].MQx = xor(_sha.ComputeHash(T1.ToByteArray()), data[0]);

            rand = gen.Generate(ElGamalModel.P.ToByteArray().Length - 2);
            result[1].Gx = BigInteger.ModPow(ElGamalModel.G, rand, ElGamalModel.P);
            T2 = BigInteger.ModPow(keys[1], rand, ElGamalModel.P);
            result[1].MQx = xor(_sha.ComputeHash(T2.ToByteArray()), data[1]);

            return result;
        }

        public byte[] DecryptData(List<ElGamalData> list)
        {
            int x = IsZero ? 0 : 1;
            T3 = BigInteger.ModPow(list[x].Gx, K, ElGamalModel.P);
            return xor(_sha.ComputeHash(T3.ToByteArray()), list[x].MQx);
        }

        private byte[] xor(byte[] key, byte[] data)
        {
            var result = new byte[key.Length];

            for(int i =0; i < key.Length; i++)
            {
                result[i] = data.Length > i ? (byte) (data[i] ^ key[i]) : key[i];
            }

            return result;
        }

        private static BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }
    }
}
