namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;

    [Serializable]
    public sealed class EmailTemplateDoesNotExistException : ResourceNotFoundException
    {
        public EmailTemplateDoesNotExistException(string message) : base(message)
        {
        }
    }
}