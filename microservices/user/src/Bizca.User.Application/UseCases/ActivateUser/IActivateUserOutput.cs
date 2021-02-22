namespace Bizca.User.Application.UseCases.ActivateUser
{
    public interface IActivateUserOutput
    {
        void NotFound(string message);
        void Invalid(string message);
    }
}
