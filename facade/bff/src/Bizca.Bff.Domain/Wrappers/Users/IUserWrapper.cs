namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IUserWrapper
    {
        Task<UserCreatedResponse> CreateUserAsync(UserToCreateRequest userRequest, IDictionary metadata = null);
        Task<UserUpdatedResponse> UpdateUserAsync(UserToUpdateRequest userRequest, IDictionary metadata = null);
    }
}