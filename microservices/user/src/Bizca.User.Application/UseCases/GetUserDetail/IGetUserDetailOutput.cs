namespace Bizca.User.Application.UseCases.GetUserDetail
{
    public interface IGetUserDetailOutput
    {
        /// <summary>
        ///     User not found.
        /// </summary>
        void NotFound();

        /// <summary>
        ///     Returns user detail.
        /// </summary>
        /// <param name="userDetail"></param>
        void Ok(GetUserDetail userDetail);
    }
}