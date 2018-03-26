using L3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3
{
    public class Problem2
    {
        public void Exe2(KeyIvModel model)
        {
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.CBC, System.Security.Cryptography.PaddingMode.None, 256);
            var rand = new Random();

            byte[] text = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] iv = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] text2 = new byte[] { 13, 65, 123, 98, 8, 4, 65, 15, 32, 47, 98, 14, 200, 10, 0, 128 };

            byte[] newcipher2;
            model.IV = iv;
            var newcipher1 = aes.EncryptToByte(text, model);

            //for (int i = 0; i < 100; i++)
            //{
            //    AddOne(text);
            //    AddOne(model.IV);
            //    var temp = aes.EncryptToByte(text, model);
            //    if (!Check(newcipher1, temp))
            //    {
            //        for (int j = 0; j < temp.Length; j++)
            //        {
            //            Console.Out.WriteLine($"{newcipher1[j]}\t{temp[j]}\t{(byte)(newcipher1[j] ^ temp[j])}");
            //        }
            //        Console.Out.WriteLine();

            //    }

            //}




            do
            {
                AddOne(text);
                AddOne(model.IV);
                AddOne(text2);
                newcipher2 = rand.NextDouble() <= 0.5 ? aes.EncryptToByte(text, model) : aes.EncryptToByte(text2, model);
            } while (Check(newcipher1, newcipher2));
            do
            {
                AddOne(text);
                AddOne(model.IV);
                var ncipher = rand.NextDouble() <= 0.5 ? aes.EncryptToByte(text, model) : aes.EncryptToByte(text2, model);
                if (Check(newcipher1, ncipher))
                {
                    Console.Out.WriteLine("pierwszy");
                    break;
                }
                else if (Check(newcipher2, ncipher))
                {
                    Console.Out.WriteLine("pierwszy");
                    newcipher1 = newcipher2;
                    break;
                }
                else Console.Out.WriteLine("drugi");
            } while (true);
            for (int i = 0; i < 100; i++)
            {
                AddOne(text);
                AddOne(model.IV);
                byte[] newcipher;
                if (rand.NextDouble() <= 0.5)
                    newcipher = aes.EncryptToByte(text, model);
                else
                    newcipher = aes.EncryptToByte(text2, model);

                if (Check(newcipher1, newcipher))
                    Console.Out.WriteLine("pierwszy");
                else Console.Out.WriteLine("drugi");
            }
        }

        public bool Check(byte[] t1, byte[] t2)
        {
            for (int i = 0; i < t1.Length; i++)
                if (t1[i] != t2[i]) return false;
            return true;
        }




        public void AddOne(byte[] array)
        {
            for (int i = array.Length; i > 0; i--)
                if (++array[i - 1] > 0) break;
        }

        public void Test(byte[] array)
        {
            for (int i = 0; i < 100000; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    Console.Write($"{array[j]} ");
                }
                Console.WriteLine();
                AddOne(array);

            }
        }

        public void Test(KeyIvModel model)
        {


            //Test(model.IV);
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.CBC, System.Security.Cryptography.PaddingMode.None, 256);

            byte[] IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] IV2 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            byte[] key = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] text = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] text2 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };

            //model.IV = IV;
            //model.Key = key;

            var c1 = aes.EncryptToByte(text, model);
            var p1 = aes.DecryptFromByte(c1, model);

            AddOne(model.IV);
            var c2 = aes.EncryptToByte(text2, model);
            var p2 = aes.DecryptFromByte(c2, model);

            for (int i = 0; i < c1.Length; i++)
            {
                Console.Out.WriteLine($"{c1[i]}\t{c2[i]}\t{(byte)(c1[i] ^ c2[i])}");
            }
        }


    }
}
