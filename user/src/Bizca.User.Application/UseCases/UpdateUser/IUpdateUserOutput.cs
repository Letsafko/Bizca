namespace Bizca.User.Application.UseCases.UpdateUser
{
    using Bizca.Core.Domain;

    public interface IUpdateUserOutput
    {
        /// <summary>
        ///     Invalid input.
        /// </summary>
        /// <param name="notification"></param>
        void Invalid(Notification notification);

        /// <summary>
        ///     User not found.
        /// </summary>
        void NotFound();

        /// <summary>
        ///     Creates an user.
        /// </summary>
        /// <param name="user">usr created.</param>
        void Ok(UpdateUserDto user);
    }
}
