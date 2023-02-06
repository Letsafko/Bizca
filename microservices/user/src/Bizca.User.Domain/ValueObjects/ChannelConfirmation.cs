namespace Bizca.User.Domain.Entities.Channel.ValueObjects
{
    using Core.Domain;
    using System;
    using System.Collections.Generic;

    public sealed class ChannelConfirmation : ValueObject
    {
        public ChannelConfirmation(string codeConfirmation, DateTime expirationDate)
        {
            CodeConfirmation = codeConfirmation;
            ExpirationDate = expirationDate;
        }

        public string CodeConfirmation { get; }

        public DateTime ExpirationDate { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CodeConfirmation;
            yield return ExpirationDate;
        }

        internal bool IsConfirmationCodeExpired(DateTime channelCodeExpiredDate)
        {
            return ExpirationDate.CompareTo(channelCodeExpiredDate) < 0;
        }
    }
}