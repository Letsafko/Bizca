namespace Bizca.Bff.Domain.ValueObject
{
    using Core.Domain;

    public sealed class Currency : Enumeration<string>
    {
        public static readonly Currency Euro = new Currency("EUR", "Euro");
        public static readonly Currency Usd = new Currency("USD", "Usd");

        private Currency(string code, string label)
            : base(code, label)
        {
        }
    }
}