namespace Bizca.User.Application.UseCases.GetUserDetail;

public interface IGetUserDetailOutput
{
    /// <summary>
    ///     User not found.
    /// </summary>
    /// <param name="message">not found message</param>
    void NotFound(string message);

    /// <summary>
    ///     Returns user detail.
    /// </summary>
    /// <param name="userDetail"></param>
    void Ok(GetUserDetail userDetail);
}