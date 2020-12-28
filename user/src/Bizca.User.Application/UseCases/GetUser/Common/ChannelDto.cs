namespace Bizca.User.Application.UseCases.GetUser.Common
{
    public sealed class ChannelDto
    {
        public ChannelDto(string value, string type, bool active, bool confirmed)
        {
            Active = active;
            Confirmed = confirmed;
            ChannelType = type;
            ChannelValue = value;
        }
        public bool Active { get; }
        public bool Confirmed { get; }
        public string ChannelType { get; }
        public string ChannelValue { get; }
    }
}
