namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    public interface IAuthenticateUserOutput
    {
        void NotFound(string message);
        void Invalid(string message);
        void Ok(AuthenticateUserDto passwordUpdated);
    }
}
