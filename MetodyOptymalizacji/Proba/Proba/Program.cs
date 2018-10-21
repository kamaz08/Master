using Proba.L1;
using System;
using System.Linq;

namespace Proba
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new Tests().StartTest(10, 10, 10000, 100);


        }
    }
}
