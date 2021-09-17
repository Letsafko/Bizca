namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Domain;
    using System.Collections;
    using System.Threading.Tasks;
    public interface IUserAuthenticationWrapper
    {
        Task<IPublicResponse<AuthenticateUserResponse>> AuthenticateUserAsync(AuthenticateUserRequest request,
            IDictionary headers = null);
    }
}