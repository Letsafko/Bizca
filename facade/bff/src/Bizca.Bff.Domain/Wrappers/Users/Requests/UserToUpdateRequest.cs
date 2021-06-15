namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    public sealed class UserToUpdateRequest
    {
        public string EconomicActivity { get; set; }
        public string ExternalUserId { get; set; }
        public string PartnerCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Civility { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
    }
}