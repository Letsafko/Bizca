namespace Bizca.User.Domain.Agregates.Users
{
    public sealed class Channel
    {
        public Channel(string value, NotificationChanels type, bool active, bool confirmed)
        {
            Active = active;
            Confirmed = confirmed;
            ChannelType = type;
            ChannelValue = value;
        }
        public bool Active { get; }
        public bool Confirmed { get; }
        public string ChannelValue { get; }
        public NotificationChanels ChannelType { get; }
    }
}
