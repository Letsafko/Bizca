namespace Bizca.Core.Domain.Country
{
    using System.Collections.Generic;
    public sealed class Country : ValueObject
    {
        public string Description { get; }
        public string CountryCode { get; }
        public int Id { get; }
        public Country(int id, string code, string description)
        {
            Description = description;
            CountryCode = code;
            Id = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Description;
            yield return CountryCode;
            yield return Id;
        }
    }
}