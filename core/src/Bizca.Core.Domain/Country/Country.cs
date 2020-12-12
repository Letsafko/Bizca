namespace Bizca.Core.Domain.Country
{
    public sealed class Country : Entity
    {
        public string CountryCode { get; }
        public string Description { get; }
        public Country(int id, string code, string description)
        {
            Id = id;
            CountryCode = code;
            Description = description;
        }
    }
}
