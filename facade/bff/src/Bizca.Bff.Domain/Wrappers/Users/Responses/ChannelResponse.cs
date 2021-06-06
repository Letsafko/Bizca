namespace Bizca.Bff.Domain.Wrappers.Users.Responses
{
    public sealed class ChannelResponse
    {
        public string ChannelValue { get; }
        public string ChannelType { get; }
        public bool Confirmed { get; }
        public bool Active { get; }
    }
}