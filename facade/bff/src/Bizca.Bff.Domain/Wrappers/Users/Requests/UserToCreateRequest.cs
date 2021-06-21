using Bizca.Bff.Domain.Properties;

namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    public sealed class UserToCreateRequest
    {
        public UserToCreateRequest(string externalUserId,
            string firstName,
            string lastName,
            int civility,
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

        public string ExternalUserId { get; }
        public string PartnerCode { get; } = Resources.PartnerCode;
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Civility { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}