namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections;
    using System.Threading.Tasks;
    public interface IUserChannelWrapper
    {
        Task<RegisterUserConfirmationCodeResponse> RegisterChannelConfirmationCodeAsync(string externalUserId,
            RegisterUserConfirmationCodeRequest request,
            IDictionary headers = null);

        Task<UserConfirmationCodeResponse> ConfirmUserChannelCodeAsync(string externalUserId,
            UserConfirmationCodeRequest request,
            IDictionary headers = null);
    }
}
