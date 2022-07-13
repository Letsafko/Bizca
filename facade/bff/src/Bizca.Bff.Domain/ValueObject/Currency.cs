using Bizca.Core.Domain;

namespace Bizca.Bff.Domain.ValueObject
{
    public sealed class Currency : Enumeration<string>
    {
        private Currency(string code, string label)
            : base(code, label)
        {
        }

        public static readonly Currency Euro = new Currency("EUR", "Euro");
        public static readonly Currency Usd = new Currency("USD", "Usd");
    }
}