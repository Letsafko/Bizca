namespace Bizca.Bff.WebApi.UseCases.V10.GetUsers
{
    using Bizca.Bff.Application.UseCases.GetUsers;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Pagination;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Get users by criteria response.
    /// </summary>
    internal sealed class GetUsersResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="GetUsersResponse"/>
        /// </summary>
        /// <param name="pagedUsers"></param>
        public GetUsersResponse(GetPagedUsersDto pagedUsers)
        {
            Relations = pagedUsers.Relations?.Select(x => new PagedLink(x.Url, x.Relation));
            Users = pagedUsers.Users.Select(x => new UserViewModel(x));
        }

        /// <summary>
        ///     Gets user list.
        /// </summary>
        public IEnumerable<UserViewModel> Users { get; }

        /// <summary>
        ///     Gets relations.
        /// </summary>
        public IEnumerable<PagedLink> Relations { get; }
    }
}
