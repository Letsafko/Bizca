namespace Bizca.User.Domain.Agregates.Users
{
    using System;
    public sealed class ChannelCodeConfirmation
    {
        public ChannelCodeConfirmation(NotificationChanels ChannelId, string channelValue, string codeConfirmation, DateTime expirationDate)
        {
            CodeConfirmation = codeConfirmation;
            ExpirationDate = expirationDate;
            ChannelValue = channelValue;
            Channel = ChannelId;
        }
        public string ChannelValue { get; }
        public NotificationChanels Channel { get; }
        public string CodeConfirmation { get; }
        public DateTime ExpirationDate { get; }
    }
}
