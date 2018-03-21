using System;
using System.Security.Cryptography;
using System.Numerics;
using System.Linq;

namespace Lista2
{
    public interface IHashFunction
    {
        BigInteger GetHash(int n);
    }
    public class HashFunction : IHashFunction
    {
        SHA256 _sHA256;
        private static readonly BigInteger maks = new BigInteger(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0 });
        
        public HashFunction()
        {
            _sHA256 = SHA256.Create();
        }

        public BigInteger GetHash(int n)
        {
            var bytes = BitConverter.GetBytes(n);
            var hash = _sHA256.ComputeHash(bytes);
            var bytelist = hash.ToList();
            bytelist.Add(0);
            return new BigInteger(bytelist.ToArray()); 
        }

        public static double Propability(BigInteger x)
            => (double) maks / (double) x;
    }
}
