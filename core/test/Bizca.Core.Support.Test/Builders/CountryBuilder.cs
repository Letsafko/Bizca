namespace Bizca.Core.Support.Test.Builders
{
    using Bizca.Core.Domain.Country;

    public sealed class CountryBuilder
    {
        private int _id;
        private string _description;
        private string _countryCode;
        private CountryBuilder()
        {
            _id = 1;
            _countryCode = "CM";
            _description = "Cameroon";
        }

        public static CountryBuilder Instance => new CountryBuilder();
        public Country Build()
        {
            return new Country(_id, _countryCode, _description);
        }

        public CountryBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CountryBuilder WithCode(string code)
        {
            _countryCode = code;
            return this;
        }

        public CountryBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
    }
}
