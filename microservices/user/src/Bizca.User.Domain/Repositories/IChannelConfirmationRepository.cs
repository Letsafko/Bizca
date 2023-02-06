namespace Bizca.User.Domain.Entities.Channel.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ValueObjects;

    public interface IChannelConfirmationRepository
    {
        Task<bool> UpsertAsync(int userId, ChannelType channelType, IEnumerable<ChannelConfirmation> channelConfirmations);
    }
}