namespace Bizca.Core.Api
{
    using Domain.Exceptions;
    using FluentValidation.Results;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class JsonSerializerExceptionFormatter : IExceptionFormatter
    {
        public string Format(IEnumerable<ValidationFailure> validationFailures)
        {
            return JsonConvert.SerializeObject(
                new ErrorResultModel(
                    "Validation failed",
                    validationFailures.Select(e =>
                        new ErrorModel(e.PropertyName, e.ErrorMessage))));
        }

        public string Format(string message, IEnumerable<DomainFailure> domainFailures)
        {
            return JsonConvert.SerializeObject(
                new ErrorResultModel(message,
                    domainFailures.Select(e =>
                        new ErrorModel(e.PropertyName, e.ErrorMessage))));
        }
    }
}