namespace Bizca.Core.Api
{
    using Domain.Exceptions;
    using FluentValidation.Results;
    using System.Collections.Generic;

    public interface IExceptionFormatter
    {
        string Format(string message, IEnumerable<DomainFailure> domainFailures);
        string Format(IEnumerable<ValidationFailure> validationFailures);
    }
}