using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyOnline.L1
{
    public class Item
    {
        public Item(int num)
        {
            Value = num;
        }
        public int Value { get; set; }
    }

    public class CountItem : Item
    {
        public CountItem(int num): base(num)
        {
            Count = 0;
        }
        public int Count { get; set; }
    }

    public interface ISelfAssembler<T> where T: Item
    {
        T[] Assmebly(T[] array, int index);
    }

    public abstract class AbstractSelfAssembler<T>
    {
        public T[] Swap<T>(T[] array, int n0, int n1)
        {
            if (n0 > n1)
            {
                var temp = n0;
                n0 = n1;
                n1 = temp;
            }
            if (n0 < 0 || array.Length <= n1) return array;

            var x = array[n0];
            array[n0] = array[n1];
            array[n1] = x;
            return array;
        }
    }
}
