namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections;
    using System.Threading.Tasks;
    public interface IUserProfileWrapper
    {
        Task<UserUpdatedResponse> UpdateUserAsync(string externalUserId,
            UserToUpdateRequest request,
            IDictionary headers = null);

        Task<UserCreatedResponse> CreateUserAsync(UserToCreateRequest request,
            IDictionary headers = null);

        Task<UserResponse> GetUserDetailsAsync(string partnerCode,
            string externalUserId,
            IDictionary headers = null);

        Task<UsersByCriteriaResponse> GetUsersByCriteriaAsync(string partnerCode,
            UsersByCriteriaRequest request,
            IDictionary headers = null);
    }
}
