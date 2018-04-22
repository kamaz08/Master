using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace L4
{
    class Program
    {
        static void Main(string[] args)
        {

            BigInteger a = 100;
            var b = BigInteger.ModPow(a, 99, 101);
            Console.Out.WriteLine(b);
            Console.In.Read();
        }
    }
}
