using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista3.Dijkstra
{
    public interface IDijkstraNextStep
    {
        List<int> GetNextSteps(int n);
    }

    public class DijkstraNextStep : IDijkstraNextStep
    {
        private int _n;
        private int[] _pow;

        public DijkstraNextStep(int n)
        {
            _n = n;
            _pow = Enumerable.Range(0, n).Select(x => (int)Math.Pow(n + 1, x)).ToArray();
        }

        private byte[] GetState(int n)
        {
            var result = new byte[_n];
            for(int i = _n -1; i >= 0; i--)
            {
                result[i] = (byte) (n / _pow[i]);
                n %= _pow[i];
            }
            return result;
        }

        public List<int> GetNextSteps(int n)
        {
            var result = new List<int>();
            var state = GetState(n);
            if (state[0] == state[_n - 1])
                result.Add(state[0] == (byte)_n ? n - _n : n + 1);
            for (int i = 1; i < _n; i++)
                if (state[i] != state[i - 1])
                    result.Add(n + _pow[i] * (state[i - 1] - state[i]));
            return result;
        }
    }
}
