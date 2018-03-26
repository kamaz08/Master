using L3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3
{
    public class Program
    {
        public static void L1()
        {
            KeyStore test = new KeyStore();

            test.LoadKeys("siemanko");

            test.GetKey(0);

            AesKrypto aesKrypto = new AesKrypto();

            aesKrypto.Encrypt("nowy.txt", "nowy.kz", test.GetKey(0));

            aesKrypto.Decrypt("nowy.kz", "nowy1.txt", test.GetKey(0));
        }

        public static void L2()
        {
            KeyStore test = new KeyStore();

            test.LoadKeys("siemanko");

            var pro = new Problem2();

            pro.Exe2(test.GetKey(0));
            //pro.Test(test.GetKey(0));


        }

        static void Main(string[] args)
        {
            L2();
            Console.ReadKey();
        }
    }
}
