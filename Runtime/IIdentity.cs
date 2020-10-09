using System;

namespace DataContainer.Runtime
{
    public interface IIdentity<T>
        where T : IEquatable<T>
    {
        T Value { get; set; }
        void Reset();
        T Increase();
        bool IsMax();
    }
}