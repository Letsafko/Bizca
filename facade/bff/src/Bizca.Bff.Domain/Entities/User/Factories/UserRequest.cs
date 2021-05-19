namespace Bizca.Bff.Domain.Entities.User.Factories
{
    using Bizca.Bff.Domain.Enumerations;
    public sealed class UserRequest
    {
        public string EconomicActivity { get; set; }
        public string ExternalUserId { get; set; }
        public string PartnerCode { get; set; }
        public string PhoneNumber { get; set; }
        public Civility Civility { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
    }
}