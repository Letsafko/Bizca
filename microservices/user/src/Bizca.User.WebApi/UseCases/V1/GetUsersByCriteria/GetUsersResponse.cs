namespace Bizca.User.WebApi.UseCases.V1.GetUsersByCriteria
{
    using Bizca.Core.Api.Modules.Pagination;
    using Bizca.User.Application.UseCases.GetUsersByCriteria;
    using Bizca.User.WebApi.ViewModels;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

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
                PagedResult<GetUsers> pagedUsers = pagination.GetPaged(criteria, pagination.FirstIndex?.UserId ?? 0, pagination.LastIndex?.UserId ?? 0);
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
        [Required]
        public List<UserModel> Users { get; }

        /// <summary>
        ///     Gets relations.
        /// </summary>
        [Required]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PagedLink> Relations { get; }
    }
}