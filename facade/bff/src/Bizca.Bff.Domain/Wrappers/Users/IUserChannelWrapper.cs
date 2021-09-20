namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Domain;
    using System.Collections;
    using System.Threading.Tasks;
    public interface IUserChannelWrapper
    {
        Task<IPublicResponse<RegisterUserConfirmationCodeResponse>> RegisterChannelConfirmationCodeAsync(RegisterUserConfirmationCodeRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<UserConfirmationCodeResponse>> ConfirmUserChannelCodeAsync(UserConfirmationCodeRequest request,
            IDictionary headers = null);
    }
}
