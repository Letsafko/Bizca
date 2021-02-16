namespace Bizca.Core.Domain.Partner
{
    public sealed class Partner : Entity
    {
        public string PartnerCode { get; }
        public string Desciption { get; }
        public FeatureFlags FeatureFlags { get; } = new FeatureFlags();
        public PartnerSettings PartnerSettings { get; } = new PartnerSettings();

        public Partner(int id, string code, string description)
        {
            Id = id;
            PartnerCode = code;
            Desciption = description;
        }
    }

    public sealed class PartnerSettings
    {
        public int ChannelCodeConfirmationExpirationDelay { get; set; } = 10;
        public int ChannelCodeConfirmationLength { get; set; } = 10;
    }

    public sealed class FeatureFlags
    {
        public bool IsPhoneMandatory { get; set; }
        public bool IsWhatsappMandatory { get; set; }
        public bool IsEmailMandatory { get; set; } = true;
        public bool IsBirthCountyMandatory { get; set; } = true;
        public bool IsEconomicActivityMandotory { get; set; }
    }
}