namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;

    [Serializable]
    public sealed class EconomicActivityDoesNotExistException : ResourceNotFoundException
    {
        public EconomicActivityDoesNotExistException(string message) : base(message)
        {
        }
    }
}