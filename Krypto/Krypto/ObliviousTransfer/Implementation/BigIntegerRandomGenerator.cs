using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ObliviousTransfer.Implementation
{
    public interface IBigIntegerRandomGenerator
    {
        BigInteger Generate(int length);
    }

    public class BigIntegerRandomGenerator : IBigIntegerRandomGenerator
    {
        private Random _random;
        public BigIntegerRandomGenerator(): this(new Random()) { }
        public BigIntegerRandomGenerator(Random random)
        {
            this._random = random;
        }

        public BigInteger Generate(int length)
        {
            byte[] data = new byte[length];
            _random.NextBytes(data);
            return BigInteger.Abs(new BigInteger(data));
        }
    }
}
