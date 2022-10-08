namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Core.Domain;
    using Requests;
    using Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IUserProfileWrapper
    {
        Task<IPublicResponse<UserUpdatedResponse>> UpdateUserAsync(UserToUpdateRequest request,
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