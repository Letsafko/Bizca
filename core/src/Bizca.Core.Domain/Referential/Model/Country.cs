namespace Bizca.Core.Domain.Referential.Model
{
    using System.Collections.Generic;

    public sealed class Country : ValueObject
    {
        public Country(int id, string code, string description)
        {
            Description = description;
            CountryCode = code;
            Id = id;
        }

        public string Description { get; }
        public string CountryCode { get; }
        public int Id { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Description;
            yield return CountryCode;
            yield return Id;
        }
    }
}