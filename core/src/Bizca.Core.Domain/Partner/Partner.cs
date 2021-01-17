namespace Bizca.Core.Domain.Partner
{
    public sealed class Partner : Entity
    {
        public string PartnerCode { get; }
        public string Desciption { get; }
        public FeatureFlags FeatureFlags { get; } = new FeatureFlags();

        public Partner(int id, string code, string description)
        {
            Id = id;
            PartnerCode = code;
            Desciption = description;
        }
    }

    public sealed class FeatureFlags
    {
        public bool IsPhoneMandatory { get; set; }
        public bool IsWhatsappMandatory { get; set; }
        public bool IsEmailMandatory { get; set; } = true;
        public bool IsUserEconomicActivityMandotory { get; set; }
    }
}