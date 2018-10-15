namespace Proba.L1
{
    public class DualPivotQuicksortYaroslavskiy : AbstractSort
    {
        public DualPivotQuicksortYaroslavskiy(int[] array) : base(array)
        {
        }

        protected override void sort(int left, int right)
        {
            if (Check(right - left >= 1))
            {
                var p = _after[left];
                var q = _after[right];

                if (Check(p > q))
                {
                    Swap(ref p, ref q);
                    Swap(ref _after[left], ref _after[right]);
                }

                var l = left + 1;
                var g = right - 1;
                var k = l;

                while (Check(k <= g))
                {
                    if (Check(_after[k] < p))
                    {
                        Swap(ref _after[k], ref _after[l]);
                        l++;
                    }
                    else
                    {
                        if (Check(_after[k] > q))
                        {
                            while (Check(_after[g] > q) && Check(k < g)) g--;
                            Swap(ref _after[k], ref _after[g]);
                            g--;
                            if (Check(_after[k] < p))
                            {
                                Swap(ref _after[k], ref _after[l]);
                                l++;
                            }
                        }
                    }
                    k++;
                }
                l--;
                g++;
                Swap(ref _after[left], ref _after[l]);
                Swap(ref _after[right], ref _after[g]);

                sort(left, l - 1);
                sort(l + 1, g - 1);
                sort(g + 1, right);
            }
        }
    }
}
