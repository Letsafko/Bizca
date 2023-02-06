namespace Bizca.User.Application.UseCases.UpdateUser;

public interface IUpdateUserOutput
{
    /// <summary>
    ///     User not found.
    /// </summary>
    /// <param name="message">not found message</param>
    void NotFound(string message);

    /// <summary>
    ///     Creates an user.
    /// </summary>
    /// <param name="user">usr created.</param>
    void Ok(UpdateUserDto user);
}