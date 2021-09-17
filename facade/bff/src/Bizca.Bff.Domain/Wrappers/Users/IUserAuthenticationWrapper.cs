namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections;
    using System.Threading.Tasks;
    public interface IUserAuthenticationWrapper
    {
        Task<AuthenticateUserResponse> AuthenticateUserAsync(AuthenticateUserRequest request,
            IDictionary headers = null);
    }
}
