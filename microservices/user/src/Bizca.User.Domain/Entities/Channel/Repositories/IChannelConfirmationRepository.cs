namespace Bizca.User.Domain.Entities.Channel.Repositories
{
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChannelConfirmationRepository
    {
        Task<bool> UpsertAsync(int userId, ChannelType channelType, IEnumerable<ChannelConfirmation> channelConfirmations);
    }
}