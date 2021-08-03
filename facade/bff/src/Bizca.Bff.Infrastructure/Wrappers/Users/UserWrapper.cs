﻿namespace Bizca.Bff.Infrastructure.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class UserWrapper : BaseWrapper, IUserWrapper
    {
        public UserWrapper(IHttpClientFactory httpClientFactory, ILogger<UserWrapper> logger)
            : base(logger, httpClientFactory, NamedHttpClients.ApiUserClientName)
        {
        }

        public async Task<UsersByCriteriaResponse> GetUsersByCriteriaAsync(string partnerCode, 
            UsersByCriteriaRequest request, 
            IDictionary headers = null)
        {
            var queryString = GetQueryString(request);
            return await SendAsync<UsersByCriteriaResponse>(HttpMethod.Get,
                $"{ApiVersion}/{partnerCode}/users{queryString}",
                metadata: headers);
        }
        
        public async Task<RegisterUserConfirmationCodeResponse> RegisterChannelConfirmationCodeAsync(string externalUserId, 
            RegisterUserConfirmationCodeRequest request, 
            IDictionary headers = null)
        {
            return await SendAsync<RegisterUserConfirmationCodeResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users/{externalUserId}/channel/code/register",
                request,
                headers);
        }

        public async Task<UserConfirmationCodeResponse> ConfirmUserChannelCodeAsync(string externalUserId, 
            UserConfirmationCodeRequest request, 
            IDictionary headers = null)
        {
            return await SendAsync<UserConfirmationCodeResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users/{externalUserId}/channel/code/confirm",
                request,
                headers);
        }

        public async Task<UserUpdatedResponse> UpdateUserAsync(string externalUserId, 
            UserToUpdateRequest request, 
            IDictionary headers = null)
        {
            return await SendAsync<UserUpdatedResponse>(HttpMethod.Put,
                $"{ApiVersion}/{request.PartnerCode}/users/{externalUserId}",
                request,
                headers);
        }

        public async Task<AuthenticateUserResponse> AutehticateUserAsync(AuthenticateUserRequest request, 
            IDictionary headers = null)
        {
            return await SendAsync<AuthenticateUserResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users/authenticate",
                request,
                headers);
        }

        public async Task<UserPasswordResponse> CreateOrUpdateUserPasswordAsync(UserPasswordRequest request, 
            IDictionary headers = null)
        {
            return await SendAsync<UserPasswordResponse>(HttpMethod.Post,
               $"{ApiVersion}/{request.PartnerCode}/users/password",
               request,
               headers);
        }

        public async Task<UserCreatedResponse> CreateUserAsync(UserToCreateRequest request, 
            IDictionary headers = null)
        {
            return await SendAsync<UserCreatedResponse>(HttpMethod.Post,
                $"{ApiVersion}/{request.PartnerCode}/users",
                request,
                headers);
        }

        #region private helpers

        private string GetQueryString<T>(T request)
        {
            const string queryStringSeparator = "?";
            const string queryStringParameterSeparator = "&";
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string jsonquery = JsonConvert.SerializeObject(request, Formatting.None, jsonSerializerSettings);
            IEnumerable<string> parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonquery)
                                                        .Select(p => $"{p.Key.ToLower()}={p.Value}");

            return parameters.Any()
                ? $"{queryStringSeparator}{string.Join(queryStringParameterSeparator, parameters)}"
                : string.Empty;
        }

        #endregion
    }
}