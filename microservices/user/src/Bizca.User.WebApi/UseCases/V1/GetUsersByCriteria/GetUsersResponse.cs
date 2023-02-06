namespace Bizca.User.WebApi.UseCases.V1.GetUsersByCriteria
{
    using Application.UseCases.GetUsersByCriteria;
    using Core.Api.Modules.Pagination;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;

    /// <summary>
    ///     Gets users response.
    /// </summary>
    public sealed class GetUsersResponse
    {
        /// <summary>
        ///     Create instance of users response.
        /// </summary>
        public GetUsersResponse(List<GetUsers> users, GetUsersByCriteria criteria, string requestPath = "")
        {
            if (users?.Any() == true)
            {
                var pagination = new Pagination<GetUsers>(criteria.PageSize, users, requestPath);
                PagedResult<GetUsers> pagedUsers = pagination.GetPaged(criteria,
                    pagination.FirstIndex?.UserId ?? 0,
                    pagination.LastIndex?.UserId ?? 0);

                Users = pagedUsers.Value.Select(x => new UserModel(x)).ToList();
                Relations = pagedUsers.Relations?.ToList();
            }
            else
            {
                Users = new List<UserModel>();
            }
        }

        /// <summary>
        ///     Gets user list.
        /// </summary>
        public List<UserModel> Users { get; }

        /// <summary>
        ///     Gets relations.
        /// </summary>
        public List<PagedLink> Relations { get; }
    }
}