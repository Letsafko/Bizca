namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    public sealed class UserToCreateRequest
    {
        public UserToCreateRequest(string externalUserId,
            string partnerCode,
            string firstName,
            string lastName,
            int civility,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Civility = civility;
            Whatsapp = whatsapp;
            Email = email;
        }

        public string EconomicActivity { get; }
        public string ExternalUserId { get; }
        public string PartnerCode { get; }
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Civility { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}