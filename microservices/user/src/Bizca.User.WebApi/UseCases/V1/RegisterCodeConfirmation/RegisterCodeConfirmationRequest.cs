namespace Bizca.User.WebApi.UseCases.V1.RegisterCodeConfirmation
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Register code confirmation request
    /// </summary>
    public record RegisterCodeConfirmationRequest(
        [Required] [property: JsonProperty("channel")] string Channel);
}
