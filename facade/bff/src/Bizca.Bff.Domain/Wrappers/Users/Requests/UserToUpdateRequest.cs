namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    using Bizca.Bff.Domain.Properties;
    public sealed class UserToUpdateRequest
    {
        public UserToUpdateRequest(string firstName,
            string lastName,
            string civility,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Civility = civility;
            Whatsapp = whatsapp;
            Email = email;
        }

        public string PartnerCode { get; } = Resources.PartnerCode;
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Civility { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
    }
}