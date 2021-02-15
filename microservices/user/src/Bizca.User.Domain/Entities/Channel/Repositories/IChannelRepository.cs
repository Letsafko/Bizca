namespace Bizca.User.Domain.Entities.Channel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChannelRepository
    {
        Task<bool> UpSertAsync(int userId, IEnumerable<Channel> channels);
    }
}