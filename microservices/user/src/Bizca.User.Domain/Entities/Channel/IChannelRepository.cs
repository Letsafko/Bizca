namespace Bizca.User.Domain.Entities.Channel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChannelRepository
    {
        Task<bool> AddAsync(int userId, IEnumerable<Channel> channels);
        Task<bool> UpdateAsync(int userId, IEnumerable<Channel> channels);
    }
}