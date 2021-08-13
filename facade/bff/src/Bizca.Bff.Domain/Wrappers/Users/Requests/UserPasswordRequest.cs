namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    public sealed class UserPasswordRequest
    {
        public UserPasswordRequest(string partnerCode,
            string password, 
            string resource)
        {
            PartnerCode = partnerCode;
            Password = password;
            Resource = resource;
        }
        public string PartnerCode { get; }
        public string Password { get; }
        public string Resource { get; }
    }
}