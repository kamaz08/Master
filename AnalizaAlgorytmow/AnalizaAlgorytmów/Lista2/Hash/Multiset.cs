using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Hash
{
    public abstract class MultisetAbstract
    {
        protected List<List<int>> _multiSetList;
        public MultisetAbstract()
        {
            _multiSetList = new List<List<int>>();
        }

        public int GetLength()
        {
            return _multiSetList.Count();
        }

        public List<int> GetMultiset(int n)
        {
            return _multiSetList[n];
        }
    }

    public class GoodMultiset : MultisetAbstract
    {
        public GoodMultiset(int begin, int end) : base()
        {
            for (int i = begin; i <= end; i++)
                _multiSetList.Add(Enumerable.Range(((begin + i - 1) * (i - 1)) / 2, i).ToList());
        }
    }

    public class BadMultiset : MultisetAbstract
    {
        public BadMultiset(int begin, int end) : base()
        {
            for (int i = begin; i <= end; i++)
                _multiSetList.Add(Enumerable.Range(begin, i).ToList());
        }
    }

    public class StupidMultiset : MultisetAbstract
    {
        public StupidMultiset(int begin, int end) : base()
        {
            for (int i = begin; i <= end; i++)
            {
                var rand = new Random();
                var list = Enumerable.Range(((begin + i - 1) * (i - 1)) / 2, i).ToList().Select(x => x * rand.Next());
                _multiSetList.Add(list.ToList());
            }
        }

    }
}
