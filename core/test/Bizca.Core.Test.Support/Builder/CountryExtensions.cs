namespace Bizca.Core.Test.Support.Builder
{
    using AutoFixture;
    using Domain.Referential.Model;

    public static class CountryExtensions
    {
        public static Country BuildNewCountry()
        {
            var fixture = new Fixture();
        
            fixture
                .Customize<Country>(c =>
                    c.FromFactory(() =>
                        new Country(fixture.Create<int>(),
                            fixture.Create<string>(),
                            fixture.Create<string>())));

            return fixture.Create<Country>();
        }
    }
}