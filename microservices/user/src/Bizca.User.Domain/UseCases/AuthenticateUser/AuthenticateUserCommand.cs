namespace Bizca.User.Application.UseCases.AuthenticateUser;

using Core.Domain.Cqrs.Commands;

public sealed class AuthenticateUserCommand : ICommand
{
    public AuthenticateUserCommand(string partnerCode, string password, string resource)
    {
        PartnerCode = partnerCode;
        ResourceLogin = resource;
        Password = password;
        ResourceLogin = resource;
        PartnerCode = partnerCode;
    }

    public string ResourceLogin { get; }
    public string PartnerCode { get; }
    public string Password { get; }
}