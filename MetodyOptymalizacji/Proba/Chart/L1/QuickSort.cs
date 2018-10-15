namespace Proba.L1
{
    public class QuickSort : AbstractSort
    {
        public QuickSort(int[] array) : base(array)
        {
        }

        protected override void sort(int left, int right)
        {
            if (Check(right - left >= 1))
            {
                int p = 0;
                Swap1(ref p, ref _after[right]);
                var i = left - 1;
                var j = right;

                do
                {
                    do i++; while (Check(_after[i] < p) && Check(i < right));
                    do j--; while (Check(_after[j] > p) && Check(j > left));
                    if (Check(j>i)) Swap(ref _after[i], ref _after[j]);
                } while (Check(j > i));
                Swap(ref _after[i], ref _after[right]);
                sort(left, i - 1);
                sort(i + 1, right);
            }
        }
    }
}
