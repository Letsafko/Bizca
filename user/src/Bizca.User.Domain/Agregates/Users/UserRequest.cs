namespace Bizca.User.Domain.Agregates.Users
{
    public sealed class UserRequest
    {
        public int PartnerId { get; set; }
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
    }
}