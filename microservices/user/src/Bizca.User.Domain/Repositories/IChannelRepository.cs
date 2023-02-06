namespace Bizca.User.Domain.Entities.Channel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChannelRepository
    {
        Task<bool> SaveAsync(int userId, IEnumerable<Channel> channels);
        Task<bool> IsExistAsync(int partnerId, string channelResource);
    }
}