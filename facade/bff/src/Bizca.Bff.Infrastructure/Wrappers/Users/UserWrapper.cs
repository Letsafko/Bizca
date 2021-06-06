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
        public UserWrapper(IHttpClientFactory httpClientFactory,
            ILogger<UserWrapper> logger) : base(logger, httpClientFactory, NamedHttpClients.ApiUserClientName)
        {
        }

        private const string ApiVersion = "v1.0";
        public async Task<UserCreatedResponse> CreateUserAsync(UserToCreateRequest request, IDictionary headers = null)
        {
            return await SendAsync<UserCreatedResponse>(HttpMethod.Post, $"{ApiVersion}/{request.PartnerCode}/users", request, headers);
        }

        public async Task<UserUpdatedResponse> UpdateUserAsync(UserToUpdateRequest request, IDictionary headers = null)
        {
            return await SendAsync<UserUpdatedResponse>(HttpMethod.Put, $"{ApiVersion}/{request.PartnerCode}/users/{request.ExternalUserId}", request, headers);
        }
    }
}