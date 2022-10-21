namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;

    [Serializable]
    public sealed class CountryDoesNotExistException : ResourceNotFoundException
    {
        public CountryDoesNotExistException(string message) : base(message)
        {
        }
    }
}