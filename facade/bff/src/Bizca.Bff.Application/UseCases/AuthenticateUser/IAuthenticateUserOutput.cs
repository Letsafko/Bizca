namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Bizca.Core.Domain;
    public interface IAuthenticateUserOutput : IPublicErrorOutput
    {
        void Ok(AuthenticateUserDto userDto);
    }
}