namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;

    [Serializable]
    public sealed class PartnerDoesNotExistException : ResourceNotFoundException
    {
        public PartnerDoesNotExistException(string message) : base(message)
        {
        }
    }
}