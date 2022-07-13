namespace Bizca.Core.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetAtomicValues();

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public override bool Equals(object obj)
        {
            return obj is ValueObject second &&
                    GetAtomicValues().SequenceEqual(second.GetAtomicValues());
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            return a is null && b is null || !(a is null) && !(b is null) && a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}