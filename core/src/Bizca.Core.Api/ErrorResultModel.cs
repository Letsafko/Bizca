namespace Bizca.Core.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public record ErrorResultModel(
        [property: JsonProperty("message")] string Message,
        [property: JsonProperty("errors")] IEnumerable<ErrorModel> Errors);
}