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
                var p = _after[right];
                var i = left - 1;
                var j = right;

                do
                {
                    do i++; while (Check(_after[i] < p) && i < right);
                    do j--; while (Check(_after[j] > p) && j > left);
                    if (Check(j>i)) Swap(ref _after[i], ref _after[j]);
                } while (j > i);
                Swap(ref _after[i], ref _after[right]);
                sort(left, i - 1);
                sort(i + 1, right);
            }
        }
    }
}
