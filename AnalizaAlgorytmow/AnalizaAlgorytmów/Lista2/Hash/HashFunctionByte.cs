using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Hash
{
    public interface IHashFunctionByte
    {
        BitArray GetHash(int n);
    }

    public class Sha256ByteFunction : IHashFunctionByte
    {
        SHA256 _sHA256;
        public Sha256ByteFunction()
        {
            _sHA256 = SHA256.Create();
        }

        public BitArray GetHash(int n)
        {
            var bytes = BitConverter.GetBytes(n);
            var hash = _sHA256.ComputeHash(bytes);
            return new BitArray(hash);
        }
    }

    public class Md5FunctionByte : IHashFunctionByte
    {
        MD5 _md5;
        public Md5FunctionByte()
        {
            _md5 = MD5.Create();
        }

        public BitArray GetHash(int n)
        {
            var bytes = BitConverter.GetBytes(n);
            var hash = _md5.ComputeHash(bytes);
            return new BitArray(hash);
        }
    }

    public class DekFunctionByte : IHashFunctionByte
    {
        public DekFunctionByte()
        {
        }

        public BitArray GetHash(int n)
        {
            var bytes = BitConverter.GetBytes(n);
            ulong  hash = (ulong)bytes.Length;
            int i = 0;

            for (i = 0; i < 8; i++)
            {
                hash = ((hash << 5) ^ (hash >> 27)) ^ (bytes[i%bytes.Length]);
            }
            return new BitArray(BitConverter.GetBytes(hash));
        }
    }


}
