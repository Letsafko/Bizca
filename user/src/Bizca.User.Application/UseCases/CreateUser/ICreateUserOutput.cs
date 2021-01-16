namespace Bizca.User.Application.UseCases.CreateUser
{
    using Bizca.Core.Domain;

    public interface ICreateUserOutput
    {
        /// <summary>
        ///     Invalid input.
        /// </summary>
        /// <param name="notification"></param>
        void Invalid(Notification notification);

        /// <summary>
        ///     Creates an user.
        /// </summary>
        /// <param name="user">usr created.</param>
        void Ok(CreateUserDto user);
    }
}
