namespace Bizca.User.Domain.Agregates.Factories
{
    using Core.Domain.Referential.Model;
    using System.Threading.Tasks;

    public interface IUserFactory
    {
        Task<IUser> BuildByPartnerAndChannelResourceAsync(Partner partner, string channelResource);
        Task<IUser> BuildByPartnerAndExternalUserIdAsync(Partner partner, string externalUserId);
        Task<IUser> CreateAsync(UserRequest request);
        Task<IUser> UpdateAsync(UserRequest request);
    }
}