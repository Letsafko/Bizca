namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Core.Domain;
    using Requests;
    using Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IUserAuthenticationWrapper
    {
        Task<IPublicResponse<AuthenticateUserResponse>> AuthenticateUserAsync(AuthenticateUserRequest request,
            IDictionary headers = null);
    }
}