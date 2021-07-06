namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    using Bizca.Bff.Domain.Properties;
    public sealed class AuthenticateUserRequest
    {
        public AuthenticateUserRequest(string password, string resource)
        {
            Password = password;
            Resource = resource;
        }
        public string PartnerCode { get; } = Resources.PartnerCode;
        public string Password { get; }
        public string Resource { get; }
    }
}