namespace Bizca.User.Application.UseCases.GetUser.Detail
{
    using Bizca.Core.Application.Abstracts;
    using Bizca.User.Application.UseCases.GetUser.Common;

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
        void Ok(GetUserDto userDetail);
    }
}
