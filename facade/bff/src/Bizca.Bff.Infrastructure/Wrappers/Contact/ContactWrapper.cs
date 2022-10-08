namespace Bizca.Bff.Infrastructure.Wrappers.Contact
{
    using Core.Domain;
    using Domain.Wrappers.Contact;
    using Domain.Wrappers.Contact.Requests;
    using Domain.Wrappers.Contact.Responses;
    using Microsoft.Extensions.Logging;
    using System.Collections;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class ContactWrapper : BaseWrapper, IContactWrapper
    {
        public ContactWrapper(IHttpClientFactory httpClientFactory, ILogger<ContactWrapper> logger)
            : base(logger, httpClientFactory, NamedHttpClients.ApiProviderName)
        {
        }

        protected override string ApiVersion { get; } = "v3";

        public async Task<IPublicResponse<CreateContactResponse>> CreateANewContactAsync(
            CreateUserContactRequest request, IDictionary headers = null)
        {
            return await SendAsync<CreateContactResponse>(HttpMethod.Post,
                $"{ApiVersion}/contacts",
                request,
                headers);
        }

        public async Task<IPublicResponse<UpdateContactResponse>> UpdateContactAsync(string email,
            UpdateUserContactRequest request, IDictionary headers = null)
        {
            return await SendAsync<UpdateContactResponse>(HttpMethod.Put,
                $"{ApiVersion}/contacts/{email}",
                request,
                headers);
        }

        public async Task<IPublicResponse<CreateContactResponse>> GetContactByEmailAsync(string email,
            IDictionary headers = null)
        {
            return await SendAsync<CreateContactResponse>(HttpMethod.Get,
                $"{ApiVersion}/contacts/{email}",
                metadata: headers);
        }

        public async Task<IPublicResponse<ContactListResponse>> CreateANewListAsync(UserContactListRequest request,
            IDictionary headers = null)
        {
            return await SendAsync<ContactListResponse>(HttpMethod.Post,
                $"{ApiVersion}/contacts/lists",
                request,
                headers);
        }
    }
}