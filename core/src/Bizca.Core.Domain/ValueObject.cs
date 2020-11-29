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
    }
}
