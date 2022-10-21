namespace Bizca.Core.Api
{
    using Newtonsoft.Json;

    public record ErrorModel(
        [property: JsonProperty("property")] string Property,
        [property: JsonProperty("error")] string Error);
}