using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4.Algo
{
    public interface ICost
    {
        int GetCost(int a, int b);
    }

    public class HiperKostkaCost : ICost
    {
        public int GetCost(int a, int b)
        {
            var result = 0;
            for (int i = 1; i < 64; i++)
            {
                if (a % 2 != b % 2)
                    result++;
                a /= 2; b /= 2;
            }
            return result;
        }
    }

    public class TorusCost : ICost
    {
        public int GetCost(int a, int b)
        {
            var result = 0;

            for(int i =0; i<3; i++)
            {
                var temp = Math.Abs(a - b) % 4;
                result += temp == 3 ? 1 : temp;  
                a /= 4;
                b /= 4;
            }
            return result;
        }
    }
}
