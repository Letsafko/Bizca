namespace Bizca.User.Domain.Entities.Channel.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class ChannelCodeConfirmationHasExpiredUserException : DomainException
    {
        public ChannelCodeConfirmationHasExpiredUserException(string message, 
            string errorCode = "code_confirmation_expired", 
            IEnumerable<DomainFailure> domainFailures = default) : base(message, errorCode, domainFailures)
        {
        }
    }
}