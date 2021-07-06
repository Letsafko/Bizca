namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    public interface IAuthenticateUserOutput
    {
        void Ok(AuthenticateUserDto userDto);
    }
}
