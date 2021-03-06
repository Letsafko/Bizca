namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    using User = Domain.Agregates.User;
    public interface IAuthenticateUserOutput
    {
        void NotFound(string message);
        void Invalid(string message);
        void Ok(User user);
    }
}
