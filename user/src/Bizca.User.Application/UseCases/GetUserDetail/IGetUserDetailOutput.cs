namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using Bizca.Core.Application.Abstracts;

    public interface IGetUserDetailOutput
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
        ///     Returns user detail.
        /// </summary>
        /// <param name="userDetail"></param>
        void Ok(GetUserDetailDto userDetail);
    }
}
