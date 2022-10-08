namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Core.Domain;
    using Requests;
    using Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IUserPasswordWrapper
    {
        Task<IPublicResponse<UserPasswordResponse>> CreateOrUpdateUserPasswordAsync(UserPasswordRequest request,
            IDictionary headers = null);
    }
}