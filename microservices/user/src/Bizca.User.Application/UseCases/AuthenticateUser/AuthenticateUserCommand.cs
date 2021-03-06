namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    using Bizca.Core.Application.Commands;
    public sealed class AuthenticateUserCommand : ICommand
    {
        public string ResourceLogin { get; }
        public string PartnerCode { get; }
        public string Password { get; }
        public AuthenticateUserCommand(string partnerCode, string password, string resource)
        {
            PartnerCode = partnerCode;
            ResourceLogin = resource;
            Password = password;
        }
    }
}