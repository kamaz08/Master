using System;
using System.Collections.Generic;
using System.Text;

namespace Proba.L1
{
    public class SortStatistic
    {
        public int NoSwap { get; set; }
        public int NoCheck { get; set; }
        public int NoElements { get; set; }
    }

    public interface ISort
    {
        SortStatistic Sort();
        void Reset(int[] array);
    }
}
