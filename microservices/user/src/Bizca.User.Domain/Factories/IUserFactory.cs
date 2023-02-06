namespace Bizca.User.Domain.Agregates.Factories;

using Core.Domain.Referential.Model;
using System.Threading.Tasks;

public interface IUserFactory
{
    Task<User> BuildByPartnerAndChannelResourceAsync(Partner partner, string channelResource);
    Task<User> BuildByPartnerAndExternalUserIdAsync(Partner partner, string externalUserId);
    Task<User> CreateAsync(UserRequest request);
    Task<User> UpdateAsync(UserRequest request);
}