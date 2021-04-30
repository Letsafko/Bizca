namespace Bizca.Bff.Domain.Referentials.Pricing.ValueObjects
{
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;

    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public Money(decimal amount)
        {
            Amount = !(amount < 0) ? amount : throw new ArgumentException(nameof(amount));
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }
    }
}
