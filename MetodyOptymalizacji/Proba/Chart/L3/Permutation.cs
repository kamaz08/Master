using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart.L3
{
    public interface IPermutation
    {
        IList<int> GetPermutation();
        void Reset();
    }

    public class Permutation : IPermutation
    {
        private List<int> _list;
        private Random _random;

        public Permutation(int n) : this(n, new Random()) { }

        public Permutation(int n, Random random)
        {
            _list = Enumerable.Range(0, n).ToList();
            _random = random;
        }

        public void Reset()
        {
            _list = _list.OrderBy(x => _random.Next()).ToList();
        }

        public IList<int> GetPermutation()
        {
            return _list;
        }
    }
}
