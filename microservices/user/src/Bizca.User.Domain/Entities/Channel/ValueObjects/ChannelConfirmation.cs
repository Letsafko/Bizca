namespace Bizca.User.Domain.Entities.Channel.ValueObjects
{
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;

    public sealed class ChannelConfirmation : ValueObject
    {
        public ChannelConfirmation(string codeConfirmation, DateTime expirationDate)
        {
            CodeConfirmation = codeConfirmation;
            ExpirationDate = expirationDate;
        }

        /// <summary>
        ///     Gets code of confirmation according to channel type.
        /// </summary>
        public string CodeConfirmation { get; }

        /// <summary>
        ///     Gets expiration date of code confirmation.
        /// </summary>
        public DateTime ExpirationDate { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CodeConfirmation;
            yield return ExpirationDate;
        }
    }
}