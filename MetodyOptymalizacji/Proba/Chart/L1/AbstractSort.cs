using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proba.L1
{
    public abstract class AbstractSort : ISort
    {
        protected int[] _before;
        protected int[] _after;
        protected int _checks = 0;
        protected int _swaps = 0;

        public AbstractSort(int[] array)
        {
            _before = array.Select(x => x).ToArray();
            _after = array.Select(x => x).ToArray();
        }

        protected bool Check(bool retValue)
        {
            _checks++;
            return retValue;
        }

        protected void Swap(ref int a, ref int b)
        {
            _swaps+=2;
            int temp = a;
            a = b;
            b = temp;
        }

        protected void Swap1(ref int a, ref int b)
        {
            _swaps++;
            a = b;
        }

        public void Reset(int[] array = null)
        {
            if (array == null)
                _after = _before.Select(x=>x).ToArray();
            else
            {
                _after = array.Select(x => x).ToArray();
                _before = array.Select(x => x).ToArray();
            }

            _checks = 0;
            _swaps = 0;
        }

        public SortStatistic Sort()
        {
            sort(0, _after.Length - 1);
            return new SortStatistic
            {
                NoCheck = _checks,
                NoSwap = _swaps,
                NoElements = _after.Length
            };
        }

        protected abstract void sort(int left, int right);
    }
}
