namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    public sealed class UsersByCriteriaRequest
    {
        public UsersByCriteriaRequest(string externalUserId,
            string phoneNumber,
            string email,
            string firstName,
            string lastName)
        {
            ExternalUserId = externalUserId;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string ExternalUserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
