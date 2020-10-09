namespace DataContainer.Runtime
{
    public struct IdentityInt : IIdentity<int>
    {
        public int Value { get; set; }

        public IdentityInt(int start = 1)
        {
            Value = start;
        }

        public void Reset()
        {
            Value = 1;
        }

        public int Increase()
        {
            var i = Value;
            Value++;
            return i;
        }

        public bool IsMax()
        {
            return Value == int.MaxValue;
        }
    }
}