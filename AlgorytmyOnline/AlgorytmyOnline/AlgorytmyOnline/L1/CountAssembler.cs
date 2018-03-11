using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgorytmyOnline.L1
{
    public class CountAssembler<T> : AbstractSelfAssembler<T>, ISelfAssembler<T> where T : CountItem
    {
        public T[] Assmebly(T[] array, int index)
        {
            array[index].Count++;
            int ptr = index;
            while (ptr > 0 && array[ptr - 1].Count < array[index].Count)
                ptr--;


            var x = Swap(array, ptr, index);
            return x;
        }
    }
}