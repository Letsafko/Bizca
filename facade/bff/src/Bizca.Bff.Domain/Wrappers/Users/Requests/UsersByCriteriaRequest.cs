namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    public sealed class UsersByCriteriaRequest
    {
        public UsersByCriteriaRequest(string externalUserId,
            string phoneNumber,
            string email,
            string firstName,
            string lastName,
            int pageSize)
        {
            ExternalUserId = externalUserId;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            PageSize = pageSize;
            Email = email;
        }

        public string ExternalUserId { get; }
        public string PhoneNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public int PageSize { get; }

    }
}
