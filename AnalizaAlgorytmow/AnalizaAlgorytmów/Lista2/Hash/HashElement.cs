using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Hash
{
    public class HashElement
    {
        public HashElement(byte[] value)
        {
            Value = value;
        }
        public byte[] Value { get; set; }
        public static bool operator <(HashElement l, HashElement r)
        {
            return CompareHash(r, l);
        }

        public static bool operator >(HashElement l, HashElement r)
        {
            return CompareHash(l, r);
        }

        public static bool CompareHash(HashElement l, HashElement r)
        {
            for (var i = 0; i < l.Value.Length; i++)
                if (l.Value[i] > r.Value[i]) return true;
                else if (l.Value[i] < r.Value[i]) return false;
            return false;
        }

        public static bool operator ==(HashElement l, HashElement r)
        {
            return EqualHash(l, r);
        }

        public static bool operator !=(HashElement l, HashElement r)
        {
            return !EqualHash(l, r);
        }

        public static bool EqualHash(HashElement l, HashElement r)
        {
            for (var i = 0; i < l.Value.Length; i++)
                if (l.Value[i] != r.Value[i]) return false;
            return true;
        }

        public static double GetPropability(byte[] l)
        {
            var result = 0.0;
            var len = l.Length-16;
            for (var i = len; i > 0; i--)
                result = (l[i - 1] + result) / 256.0;
            return result;
        }
    }
}
