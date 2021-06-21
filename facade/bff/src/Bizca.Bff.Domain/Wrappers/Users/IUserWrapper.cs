namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IUserWrapper
    {
        Task<UserConfirmationCodeResponse> RegisterChannelConfirmationCodeAsync(string externalUserId, RegisterUserConfirmationCodeRequest request, IDictionary headers = null);
        Task<UserUpdatedResponse> UpdateUserAsync(string externalUserId, UserToUpdateRequest request, IDictionary headers = null);
        Task<UserCreatedResponse> CreateUserAsync(UserToCreateRequest request, IDictionary headers = null);
    }
}