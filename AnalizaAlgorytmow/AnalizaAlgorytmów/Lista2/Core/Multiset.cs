using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Core
{
    public abstract class MultisetAbstract<T>
    {
        protected List<List<T>> _multiSetList;
        public MultisetAbstract()
        {
            _multiSetList = new List<List<T>>();
        }

        public int GetLength()
        {
            return _multiSetList.Count();
        }

        public List<T> GetMultiset(int n)
        {
            return _multiSetList[n];
        }
    }

    public class GoodMultiset : MultisetAbstract<int>
    {
        public GoodMultiset(int begin, int end) : base()
        {
            for (int i = begin; i <= end; i++)
                _multiSetList.Add(Enumerable.Range(((begin + i - 1) * (i - 1)) / 2, i).ToList());
        }
    }

    public class BadMultiset : MultisetAbstract<int>
    {
        public BadMultiset(int begin, int end) : base()
        {
            for (int i = begin; i <= end; i++)
                _multiSetList.Add(Enumerable.Range(begin, i).ToList());
        }
    }
}
