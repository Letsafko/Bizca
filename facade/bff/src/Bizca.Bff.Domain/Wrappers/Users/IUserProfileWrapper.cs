namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Domain;
    using System.Collections;
    using System.Threading.Tasks;
    public interface IUserProfileWrapper
    {
        Task<IPublicResponse<UserUpdatedResponse>> UpdateUserAsync(string externalUserId,
            UserToUpdateRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<UserCreatedResponse>> CreateUserAsync(UserToCreateRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<UserResponse>> GetUserDetailsAsync(string partnerCode,
            string externalUserId,
            IDictionary headers = null);

        Task<IPublicResponse<UsersByCriteriaResponse>> GetUsersByCriteriaAsync(string partnerCode,
            UsersByCriteriaRequest request,
            IDictionary headers = null);
    }
}