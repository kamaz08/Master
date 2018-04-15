using System;
//using System.Security.Cryptography;
using System.Numerics;
using System.Linq;

namespace Lista2.Core
{
    public interface IHashFunction
    {
        BigInteger GetHash(int n);
        double GetPropa(BigInteger n);
    }
    public class HashFunction : IHashFunction
    {
        Org.BouncyCastle.Crypto.Digests.Sha256Digest myHash = new Org.BouncyCastle.Crypto.Digests.Sha256Digest();
        private static readonly BigInteger maks = new BigInteger(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0 });

        public BigInteger GetHash(int n)
        {
            var encData = BitConverter.GetBytes(n);

            myHash.BlockUpdate(encData, 0, encData.Length);
            byte[] compArr = new byte[myHash.GetDigestSize()];
            myHash.DoFinal(compArr, 0);
            var x = compArr.ToList();
            x.Add(0);
            return new BigInteger(x.ToArray());
        }
        public double GetPropa(BigInteger n)
        => (double)maks / (double)n;
    }
}
