namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Domain;
    using System.Collections;
    using System.Threading.Tasks;
    public interface IUserPasswordWrapper
    {
        Task<IPublicResponse<UserPasswordResponse>> CreateOrUpdateUserPasswordAsync(UserPasswordRequest request,
            IDictionary headers = null);
    }
}
