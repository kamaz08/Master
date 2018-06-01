using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ObliviousTransfer.Model
{
    public class ElGamalModel
    {
        public BigInteger P { get; set; }
        public BigInteger G { get; set; }
        public BigInteger Private { get; set; }
        public BigInteger Public { get; set; }
    }

    public class ElGamalData
    {
        public BigInteger Gx { get; set; }
        public Byte[] MQx { get; set; }
    }
}
