namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    using Properties;

    public sealed class UserToUpdateRequest
    {
        public UserToUpdateRequest(string externalUserId,
            string firstName,
            string lastName,
            string civility,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            ExternalUserId = externalUserId;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Civility = civility;
            Whatsapp = whatsapp;
            Email = email;
        }

        public string PartnerCode { get; } = Resources.PartnerCode;
        public string ExternalUserId { get; }
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Civility { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}