using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chart.L4
{
    public static class Mean
    {
        public static double GetHarmonicMean(this IList<double> list)
        {
            return list.Count / list.Sum(x => 1 / x);
        }

        public static double GetGeometricMean(this IList<double> list)
        {
            return Math.Pow(list.Aggregate((a,x) => a*x) , 1.0 / list.Count);
        }

        public static  double GetArytmeticMean(this IList<double> list)
        {
            return list.Sum(x => x) / list.Count;
        }
    }
}
