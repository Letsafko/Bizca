namespace Bizca.User.Domain.Agregates.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByPartnerIdAndChannelResourceAsync(int partnerId, string channelResource);
        Task<Dictionary<ResultName, IEnumerable<dynamic>>> GetByPartnerIdAndExternalUserIdAsync(int partnerId, string externalUserId);
        Task<IEnumerable<dynamic>> GetByCriteriaAsync(int partnerId, UserCriteria criteria);
        Task<bool> IsExistAsync(int partnerId, string externalUserId);
        Task<int> UpdateAsync(User user);
        Task<int> AddAsync(User user);
    }

    public enum ResultName
    {
        User,
        Addresses,
        Passwords,
        ChannelConfirmations
    }
}