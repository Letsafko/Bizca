namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Bizca.User.Domain.Agregates.Users;
    public sealed class ConfirmChannelCodeDto
    {
        public ConfirmChannelCodeDto(NotificationChanels channelId, string channelValue, bool confirmed)
        {
            Channel = channelId;
            ChannelValue = channelValue;
            ChannelConfirmed = confirmed;
        }

        public NotificationChanels Channel { get; }
        public string ChannelValue { get; }
        public bool ChannelConfirmed { get; }
    }
}
