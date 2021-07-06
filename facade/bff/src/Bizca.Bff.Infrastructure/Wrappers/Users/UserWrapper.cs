namespace Bizca.Bff.Infrastructure.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Microsoft.Extensions.Logging;
    using System.Collections;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class UserWrapper : BaseWrapper, IUserWrapper
    {
        public UserWrapper(IHttpClientFactory httpClientFactory, ILogger<UserWrapper> logger)
            : base(logger, httpClientFactory, NamedHttpClients.ApiUserClientName)
        {
        }

        public async Task<RegisterUserConfirmationCodeResponse> RegisterChannelConfirmationCodeAsync(string externalUserId, RegisterUserConfirmationCodeRequest request, IDictionary headers = null)
        {
            return await SendAsync<RegisterUserConfirmationCodeResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users/{externalUserId}/channel/code/register",
                request,
                headers);
        }

        public async Task<UserConfirmationCodeResponse> ConfirmUserChannelCodeAsync(string externalUserId, UserConfirmationCodeRequest request, IDictionary headers = null)
        {
            return await SendAsync<UserConfirmationCodeResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users/{externalUserId}/channel/code/confirm",
                request,
                headers);
        }

        public async Task<UserUpdatedResponse> UpdateUserAsync(string externalUserId, UserToUpdateRequest request, IDictionary headers = null)
        {
            return await SendAsync<UserUpdatedResponse>(HttpMethod.Put,
                $"{ApiVersion}/{request.PartnerCode}/users/{externalUserId}",
                request,
                headers);
        }

        public async Task<AuthenticateUserResponse> AutehticateUserAsync(AuthenticateUserRequest request, IDictionary headers = null)
        {
            return await SendAsync<AuthenticateUserResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users/authenticate",
                request,
                headers);
        }

        public async Task<UserCreatedResponse> CreateUserAsync(UserToCreateRequest request, IDictionary headers = null)
        {
            return await SendAsync<UserCreatedResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users",
                request,
                headers);
        }
    }
}