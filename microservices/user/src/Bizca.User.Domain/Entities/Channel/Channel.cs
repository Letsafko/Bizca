namespace Bizca.User.Domain.Entities.Channel
{
    using Bizca.User.Domain.ValueObjects;
    public sealed class Channel
    {
        public Channel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            Active = active;
            ChannelValue = value;
            Confirmed = confirmed;
            ChannelType = channelType;
        }
        public bool Active { get; }
        public bool Confirmed { get; }
        public string ChannelValue { get; }
        public ChannelType ChannelType { get; }
    }
}