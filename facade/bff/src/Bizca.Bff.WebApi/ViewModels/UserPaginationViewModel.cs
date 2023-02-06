namespace Bizca.Bff.WebApi.ViewModels
{
    using Application.UseCases.GetUsers;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     User pagination view model.
    /// </summary>
    public sealed class UserPaginationViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="UserPaginationViewModel" />
        /// </summary>
        /// <param name="pagedUsers"></param>
        public UserPaginationViewModel(GetPagedUsersDto pagedUsers)
        {
            Relations = pagedUsers.Relations?.Select(x => new PaginationLinkViewModel(x.Url, x.Relation));
            Users = pagedUsers.Users.Select(x => new UserViewModel(x));
        }

        /// <summary>
        ///     Gets user list.
        /// </summary>
        public IEnumerable<UserViewModel> Users { get; }

        /// <summary>
        ///     Gets relations.
        /// </summary>
        public IEnumerable<PaginationLinkViewModel> Relations { get; }
    }
}