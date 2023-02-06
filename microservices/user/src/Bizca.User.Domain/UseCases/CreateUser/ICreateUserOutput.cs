namespace Bizca.User.Application.UseCases.CreateUser;

public interface ICreateUserOutput
{
    /// <summary>
    ///     Creates an user.
    /// </summary>
    /// <param name="user">user created.</param>
    void Ok(CreateUserDto user);
}