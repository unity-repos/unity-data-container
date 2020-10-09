namespace DataContainer.Runtime
{
    public struct IdentityUlong : IIdentity<ulong>
    {
        public ulong Value { get; set; }

        public IdentityUlong(ulong start = 1)
        {
            Value = start;
        }

        public void Reset()
        {
            Value = 1;
        }

        public ulong Increase()
        {
            var i = Value;
            Value++;
            return i;
        }

        public bool IsMax()
        {
            return Value == ulong.MaxValue;
        }
    }
}