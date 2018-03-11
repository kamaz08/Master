namespace AlgorytmyOnline.L1
{
    public class LinearPropability : NumberPropability
    {
        public LinearPropability() : base() { }

        public override int GetNextInt()
        {
            return _random.Next(100) + 1;
        }
    }
}
