namespace Bizca.Bff.Domain.Wrappers.Users
{
    using Core.Domain;
    using Requests;
    using Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IUserChannelWrapper
    {
        Task<IPublicResponse<RegisterUserConfirmationCodeResponse>> RegisterChannelConfirmationCodeAsync(
            RegisterUserConfirmationCodeRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<UserConfirmationCodeResponse>> ConfirmUserChannelCodeAsync(
            UserConfirmationCodeRequest request,
            IDictionary headers = null);
    }
}