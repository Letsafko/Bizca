namespace Bizca.Core.Domain.Partner
{
    public sealed class Partner : Entity
    {
        public string PartnerCode { get; }
        public string Desciption { get; }

        public Partner(int id, string code, string description)
        {
            Id = id;
            PartnerCode = code;
            Desciption = description;
        }
    }
}
