using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace L4.Crypto
{
    public class CryptoModel
    {
        public List<BigInteger> PublicKey { get; set; }
        public List<BigInteger> PrivateKey { get; set; }
        public BigInteger BlockSize { get; set; }
        public BigInteger Q;
        public BigInteger R;
    }
}
