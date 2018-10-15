using System.Linq;

namespace Proba.Random
{
    public interface IRandomNumber
    {
        int[] GetRandomArray(int sizeArray, int? diffrentValues = null);
    }

    public class RandomNumber : IRandomNumber
    {
        private System.Random _random;

        public RandomNumber() : this(new System.Random())
        {

        }

        public RandomNumber(System.Random random)
        {
            _random = random;
        }

        public int[] GetRandomArray(int n, int? diffrentValues = null)
        {
            return Enumerable
                .Repeat(0, n)
                .Select(i => _random.Next(0, diffrentValues ?? n))
                .ToArray();
        }
    }
}
