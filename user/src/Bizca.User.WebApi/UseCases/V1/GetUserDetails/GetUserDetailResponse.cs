namespace Bizca.User.WebApi.UseCases.V1.GetUserDetails
{
    using Bizca.User.Application.UseCases.GetUser.Common;
    using Bizca.User.WebApi.ViewModels;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Gets user detail response.
    /// </summary>
    public sealed class GetUserDetailResponse
    {
        /// <summary>
        ///     Create instance of user detail response.
        /// </summary>
        public GetUserDetailResponse(GetUserDto userDetail)
        {
            User = new UserDetailsModel(userDetail);
        }

        /// <summary>
        ///     Get user code.
        /// </summary>
        [Required]
        public UserDetailsModel User { get; }
    }
}
