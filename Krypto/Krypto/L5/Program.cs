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

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        static void Main(string[] args)
        {
            string one = "d131dd02c5e6eec4693d9a0698aff95c2fcab58712467eab4004583eb8fb7f8955ad340609f4b30283e488832571415a085125e8f7cdc99fd91dbdf280373c5bd8823e3156348f5bae6dacd436c919c6dd53e2b487da03fd02396306d248cda0e99f33420f577ee8ce54b67080a80d1ec69821bcb6a8839396f9652b6ff72a70";
            string two = "d131dd02c5e6eec4693d9a0698aff95c2fcab50712467eab4004583eb8fb7f8955ad340609f4b30283e4888325f1415a085125e8f7cdc99fd91dbd7280373c5bd8823e3156348f5bae6dacd436c919c6dd53e23487da03fd02396306d248cda0e99f33420f577ee8ce54b67080280d1ec69821bcb6a8839396f965ab6ff72a70";

            var a = StringToByteArray(one);
            var b = StringToByteArray(two);

            var a1 = MD5Func(a);
            var b1 = MD5Func(b);

            var wynik = Check(a1, b1);

            byte[] file;

            File.WriteAllBytes("plik1", a);
            File.WriteAllBytes("plik2", b);

            using (var stream = File.OpenRead("grade2.txt"))
                file = ReadFully(stream);
            Conflict(file);
        }
    }
}

//3a46c035a1a68a8bed13a09e33ea1de8
//3a46c035a1a68a8bed13a09e33ea1de8