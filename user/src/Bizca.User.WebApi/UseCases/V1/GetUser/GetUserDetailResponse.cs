namespace Bizca.User.WebApi.UseCases.V1.GetUser
{
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.WebApi.ViewModels;

    /// <summary>
    ///     Gets user detail response.
    /// </summary>
    public sealed class GetUserDetailResponse
    {
        /// <summary>
        ///     Create instance of user detail response.
        /// </summary>
        public GetUserDetailResponse(GetUserDetailDto userDetail)
        {
            User = new UserDetailsModel(userDetail);
        }

        /// <summary>
        ///     Gets user detail.
        /// </summary>
        public UserDetailsModel User { get; }
    }
}
