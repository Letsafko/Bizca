namespace Bizca.User.Application.UseCases.UpdateUser
{
    public interface IUpdateUserOutput
    {
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