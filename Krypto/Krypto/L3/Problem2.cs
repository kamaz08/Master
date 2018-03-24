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
        public void Test(KeyIvModel model)
        {
            AesKrypto aes = new AesKrypto(System.Security.Cryptography.CipherMode.CBC, System.Security.Cryptography.PaddingMode.None, 256);

            byte[] IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] key = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] text = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] text2 = new byte[] { 34, 33, 34, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            model.IV = IV;
            model.Key = key;

            var c1 = aes.EncryptToByte(text, model);
            var p1 = aes.DecryptFromByte(c1, model);

            var c11 = aes.EncryptToByte(text2, model);
            var p11 = aes.DecryptFromByte(c11, model);

            for(int i =0; i<c1.Length; i++)
            {
                Console.Out.WriteLine($"{c1[i]}\t{c11[i]}\t{(byte)(c1[i]^c11[i])}");
            }

            // model.IV[15]++;
            var c2 = aes.EncryptToByte(c1, model);
            var p2 = aes.DecryptFromByte(c2, model);

            model.IV[15]++;
            var c3 = aes.EncryptToByte(p2, model);
            var p3 = aes.DecryptFromByte(c3, model);

            for (int i =0; i< c1.Length; i++)
            {
                Console.Out.WriteLine($"{c1[i]}\t{c2[i]}\t{c3[i]}");
            }


        }


    }
}
