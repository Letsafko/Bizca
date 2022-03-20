namespace Bizca.Bff.Domain.Wrappers.Contact
{
    using Bizca.Bff.Domain.Wrappers.Contact.Requests;
    using Bizca.Bff.Domain.Wrappers.Contact.Responses;
    using Bizca.Core.Domain;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IContactWrapper
    {
        Task<IPublicResponse<UpdateContactResponse>> UpdateContactAsync(string email, UpdateUserContactRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<CreateContactResponse>> CreateANewContactAsync(CreateUserContactRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<ContactListResponse>> CreateANewListAsync(UserContactListRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<CreateContactResponse>> GetContactByEmailAsync(string email,
            IDictionary headers = null);
    }
}
