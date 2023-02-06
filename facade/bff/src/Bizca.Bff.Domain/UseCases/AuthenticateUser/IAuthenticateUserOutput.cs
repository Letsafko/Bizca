namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Core.Domain;

    public interface IAuthenticateUserOutput : IPublicErrorOutput
    {
        void Ok(AuthenticateUserDto userDto);
    }
}