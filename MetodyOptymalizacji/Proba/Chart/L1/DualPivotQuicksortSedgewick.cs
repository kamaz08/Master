namespace Proba.L1
{
    public class DualPivotQuicksortSedgewick : AbstractSort
    {
        public DualPivotQuicksortSedgewick(int[] array) : base(array)
        {
        }

        protected override void sort(int left, int right)
        {
            if (Check(right - left >= 1))
            {
                var i = left;
                var i1 = left;
                var j = right;
                var j1 = right;

                var p = _after[left];
                var q = _after[right];

                Swap1(ref p, ref _after[left]);
                Swap1(ref q, ref _after[right]);

                if (Check(p > q)) Swap(ref p, ref q);
                bool end = false;
                while (true)
                {
                    i++;
                    while (Check(_after[i] <= q))
                    {
                        if (Check(i >= j))
                        {
                            end = true;
                            break;
                        }
                        if (Check(_after[i] < p))
                        {
                            Swap1(ref _after[i1], ref _after[i]);
                            i1++;
                            Swap1(ref _after[i], ref _after[i1]);
                        }
                        i++;
                    }
                    if (end) break;
                    j--;

                    while (Check(_after[j] >= p))
                    {
                        if (Check(_after[j] > q))
                        {
                            Swap1(ref _after[j1], ref _after[j]);
                            j1--;
                            Swap1(ref _after[j], ref _after[j1]);

                        }
                        if (Check(i >= j))
                        {
                            end = true;
                            break;
                        }
                        j--;
                    }
                    if (end) break;

                    Swap1(ref _after[i1], ref _after[j]);
                    Swap1(ref _after[j1], ref _after[i]);
                    i1++;
                    j1--;
                    Swap1(ref _after[i], ref _after[i1]);
                    Swap1(ref _after[j], ref _after[j1]);
                }
                Swap1(ref _after[i1], ref p);
                Swap1(ref _after[j1], ref q);
                sort(left, i1 - 1);
                sort(i1 + 1, j1 - 1);
                sort(j1 + 1, right);
            }
        }
    }
}
