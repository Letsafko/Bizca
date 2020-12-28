namespace Bizca.User.Application.UseCases.GetUser.List
{
    using Bizca.Core.Application.Abstracts;
    using Bizca.Core.Application.Abstracts.Paging;
    using Bizca.User.Application.UseCases.GetUser.Common;

    public interface IGetUsersOutput
    {
        /// <summary>
        ///     Invalid input.
        /// </summary>
        /// <param name="notification"></param>
        void Invalid(Notification notification);

        /// <summary>
        ///     Returns user detail.
        /// </summary>
        /// <param name="userDetail"></param>
        void Ok(PagedResult<GetUserDto> pagedUsers);
    }
}
