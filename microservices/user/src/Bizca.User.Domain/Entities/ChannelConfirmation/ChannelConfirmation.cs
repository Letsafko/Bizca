namespace Bizca.User.Domain.Entities.ChannelConfirmation
{
    using Bizca.User.Domain.ValueObjects;
    using System;
    public sealed class ChannelConfirmation
    {
        public ChannelConfirmation(ChannelType channelType, string codeConfirmation, DateTime expirationDate)
        {
            CodeConfirmation = codeConfirmation;
            ExpirationDate = expirationDate;
            ChannelType = channelType;
        }

        /// <summary>
        ///     Gets channel type.
        /// </summary>
        public ChannelType ChannelType { get; }

        /// <summary>
        ///     Gets code of confirmation according to channel type.
        /// </summary>
        public string CodeConfirmation { get; }

        /// <summary>
        ///     Gets expiration date of code confirmation.
        /// </summary>
        public DateTime ExpirationDate { get; }
    }
}