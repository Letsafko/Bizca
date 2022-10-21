namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;

    [Serializable]
    public sealed class CivilityDoesNotExistException : ResourceNotFoundException
    {
        public CivilityDoesNotExistException(string message) : base(message)
        {
        }
    }
}