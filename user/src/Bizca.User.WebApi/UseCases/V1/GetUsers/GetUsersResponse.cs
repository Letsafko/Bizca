namespace Bizca.User.WebApi.UseCases.V1.GetUsers
{
    using Bizca.Core.Application.Abstracts.Paging;
    using Bizca.User.Application.UseCases.GetUser.Common;
    using Bizca.User.WebApi.ViewModels;
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
        public GetUsersResponse(PagedResult<GetUserDto> pagedUsers)
        {
            Relations = pagedUsers.Relations?.ToList();
            if(pagedUsers.Value?.Any() == true)
            {
                foreach (GetUserDto user in pagedUsers.Value)
                {
                    var userModel = new UserModel(user);
                    Users.Add(userModel);
                }
            }
        }

        /// <summary>
        ///     Gets user list.
        /// </summary>
        [Required]
        public List<UserModel> Users { get; } = new List<UserModel>();

        /// <summary>
        ///     Gets relations.
        /// </summary>
        [Required]
        public List<PagedLink> Relations { get; }
    }
}
