using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace L4.Crypto
{
    public class MerkleHellmanGenerator
    {
        private Random _random;
        private int _randomNumber;
        public MerkleHellmanGenerator(int rand)
        {
            _randomNumber = rand;
            _random = new Random();
        }


        public CryptoModel Generate(int n = 8)
        {
            var result = new CryptoModel();
            result.BlockSize = n;
            result.PrivateKey = new List<BigInteger>();

            BigInteger last = 1;
            BigInteger sum = 1;
            for (int i = 0; i < n; i++)
            {
                last = sum + _random.Next(_randomNumber) + 1;
                sum += last;
                result.PrivateKey.Add(last);
            }

            var size = (int) (result.Q / 10);

            result.Q = sum + _random.Next(size);
            result.R = result.Q / 2 + _random.Next(size);
            while (BigInteger.GreatestCommonDivisor(result.Q, result.R) != 1)
                result.R = result.Q / 2 + _random.Next(100);

            result.PublicKey = result.PrivateKey
                .Select(x => (x * result.R) % result.Q)
                .ToList();

            return result;
        }
    }
}
