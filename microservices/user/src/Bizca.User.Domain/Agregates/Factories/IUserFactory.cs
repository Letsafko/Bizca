namespace Bizca.User.Domain.Agregates.Factories
{
    using Bizca.Core.Domain.Partner;
    using System.Threading.Tasks;

    public interface IUserFactory
    {
        Task<IUser> BuildByPartnerAndChannelResourceAsync(Partner partner, string channelResource);
        Task<IUser> BuildByPartnerAndExternalUserIdAsync(Partner partner, string externalUserId);
        Task<IUser> CreateAsync(UserRequest request);
        Task<IUser> UpdateAsync(UserRequest request);
    }
}