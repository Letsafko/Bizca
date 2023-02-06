namespace Bizca.Core.Test.Support.Builder
{
    using AutoFixture;
    using Domain.Referential.Enums;
    using Domain.Referential.Model;

    public static class PartnerExtensions
    {
        public static Partner BuildNewPartner()
        {
            var fixture = new Fixture();
        
            fixture
                .Customize<PartnerConfiguration>(c =>
                    c.FromFactory(() =>
                        new PartnerConfiguration(fixture.Create<MandatoryUserProfileField>(),
                            fixture.Create<MandatoryAddressField>(),
                            24 * 60,
                            10)));
            fixture
                .Customize<Partner>(
                    c =>
                        c.FromFactory(() => new Partner(fixture.Create<int>(),
                            "Bizca",
                            "Bizca",
                            fixture.Create<PartnerConfiguration>())));

            return fixture.Create<Partner>();
        }
    }
}