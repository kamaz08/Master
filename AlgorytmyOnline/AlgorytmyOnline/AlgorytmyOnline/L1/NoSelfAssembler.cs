using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgorytmyOnline.L1
{
    public class NoSelfAssembler<T> : ISelfAssembler<T> where T: Item
    {
        public T[] Assmebly(T[] array, int index)
        {
            return array;
        }
    }
}