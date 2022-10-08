namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    using Domain.Agregates;

    public interface IAuthenticateUserOutput
    {
        void NotFound(string message);
        void Invalid(string message);
        void Ok(User user);
    }
}