namespace Bizca.Bff.Domain.Wrappers.Users.Responses
{
    public sealed class ChannelResponse
    {
        public string ChannelValue { get; set; }
        public string ChannelType { get; set; }
        public bool Confirmed { get; set; }
        public bool Active { get; set; }
    }
}