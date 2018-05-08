using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace L5
{
    class Program
    {
        public static byte[] hash = new byte[] { 58, 70, 192, 53, 161, 166, 138, 139, 237, 19, 160, 158, 51, 234, 29, 232 };
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] MD5Func(byte[] file)
        {
            byte[] result;
            using (var md5 = MD5.Create())
            {
                result = md5.ComputeHash(file);
            }
            return result;
        }

        public static void Conflict(byte[] file)
        {
            var length = file.Length;
            //var temp = file.ToList();
            var md5res = MD5Func(file);
            while (Check(md5res, hash) == false)
            {
                file = add(file, length);
                md5res = MD5Func(file);
            }

            File.WriteAllBytes("wynik2suuper.txt", file);


        }

        public static byte[] add(byte[] file, int length)
        {
            if (file.Length < length + 1)
            {
                var temp2 = file.ToList();
                temp2.Add(0);
                return temp2.ToArray();
            }
            for (int i = file.Length - 1; i >= length; i--)
            {
                if (file[i] < 255)
                {
                    file[i]++;
                    return file;
                }
                else
                    file[i] = 0;
            }
            var temp = file.ToList();
            temp.Add(0);
            return temp.ToArray();
        }


        public static bool Check(byte[] a, byte[] b)
        {
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }




        static void Main(string[] args)
        {
            byte[] file;

            using (var stream = File.OpenRead("grade2.txt"))
                file = ReadFully(stream);
            Conflict(file);
        }
    }
}

//3a46c035a1a68a8bed13a09e33ea1de8
//3a46c035a1a68a8bed13a09e33ea1de8