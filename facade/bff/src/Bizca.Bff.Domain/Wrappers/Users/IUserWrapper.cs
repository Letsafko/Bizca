namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IUserWrapper
    {
        Task<RegisterUserConfirmationCodeResponse> RegisterChannelConfirmationCodeAsync(string externalUserId, RegisterUserConfirmationCodeRequest request, IDictionary headers = null);
        Task<UserConfirmationCodeResponse> ConfirmUserChannelCodeAsync(string externalUserId, UserConfirmationCodeRequest request, IDictionary headers = null);
        Task<UserUpdatedResponse> UpdateUserAsync(string externalUserId, UserToUpdateRequest request, IDictionary headers = null);
        Task<UserPasswordResponse> CreateOrUpdateUserPasswordAsync(UserPasswordRequest request, IDictionary headers = null);
        Task<AuthenticateUserResponse> AutehticateUserAsync(AuthenticateUserRequest request, IDictionary headers = null);
        Task<UserCreatedResponse> CreateUserAsync(UserToCreateRequest request, IDictionary headers = null);
    }
}