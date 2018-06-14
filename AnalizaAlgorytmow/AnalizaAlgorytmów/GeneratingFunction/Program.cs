using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratingFunction
{
    class Program
    {
        Random _radnom = new Random();

        static void Main(string[] args)
        {
            int n = 3;

            Program a = new Program();

            var result = new List<double>();


            

            for(int i =2; i < 10; i++)
            {
                var stepResult = new List<int>();
                for(int j = 0; j < 10000; j++)
                    stepResult.Add(a.Step(i));
                result.Add(stepResult.Average());
            }


            result.ForEach(x =>
            {
                Console.WriteLine(x);
            });
        }

        public int Step(int n)
        {
            if (n < 2) return 1;
            int result = 1;
            Enumerable.Range(1, n).ToList().ForEach(x =>
            {
                if (_radnom.NextDouble() > 0.5)
                    result += Step(x);
            });
            return result;
        }
    }
}
