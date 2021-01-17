namespace Bizca.User.Domain.Entities.ChannelConfirmation
{
    using Bizca.User.Domain.ValueObjects;
    using System.Collections.Generic;

    public interface IChannelConfirmationFactory
    {
        ChannelConfirmation Create(string externalUserId, ChannelType requestedChannelType, IEnumerable<ChannelType> channelTypes);
    }
}