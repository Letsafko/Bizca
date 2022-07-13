namespace Bizca.Bff.Domain.Provider.Campaign
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("campaign", Schema = "bff")]
    public sealed class Compaign
    {
        public CompaignType CompaignType { get; set; }
        public int CompaignId { get; set; }
        public string Name { get; set; }
        public int ListId { get; set; }
    }

    public enum CompaignType
    {
        Email,
        Sms
    }
}