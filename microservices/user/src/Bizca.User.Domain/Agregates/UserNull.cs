namespace Bizca.User.Domain.Agregates
{
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    public sealed class UserNull : IUser
    {
        public static UserNull Instance { get; } = new UserNull();
        public UserCode UserCode => new UserCode(Guid.Empty);
        public void AddChannelCodeConfirmation(ChannelType channelType)
        {
            throw new NotImplementedException();
        }

        public void AddChannel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Channel GetChannel(ChannelType channelType)
        {
            throw new NotImplementedException();
        }

        public void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public void UpdateChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation)
        {
            throw new NotImplementedException();
        }
    }
}