namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    using Bizca.Core.Application.Commands;
    public sealed class AuthenticateUserCommand : ICommand
    {
        public string PartnerCode { get; }
        public string ExternalUserId { get; }
        public string Password { get; }
        public string ResourceLogin { get; }
        public AuthenticateUserCommand(string partnerCode, string externalUserId, string password, string resource)
        {
            Password = password;
            ResourceLogin = resource;
            PartnerCode = partnerCode;
            ExternalUserId = externalUserId;
        }
    }
}