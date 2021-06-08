namespace Bizca.Bff.Domain.Referentials.Bundle.ValueObjects
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using System.Collections.Generic;
    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public Money(decimal amount)
        {
            Amount = !(amount < 0) ? amount : throw new DomainException($"{nameof(amount)} should be greater than 0.");
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }
    }
}
