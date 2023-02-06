namespace Bizca.Bff.Domain.ValueObject
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using System.Collections.Generic;

    public sealed class Money : ValueObject
    {
        public Money(decimal amount, Currency currency)
        {
            Amount = !(amount < 0) ? amount : throw new DomainException($"{nameof(amount)} should be greater than 0.");
            Currency = currency;
        }

        public Currency Currency { get; }
        public decimal Amount { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Currency;
            yield return Amount;
        }
    }
}