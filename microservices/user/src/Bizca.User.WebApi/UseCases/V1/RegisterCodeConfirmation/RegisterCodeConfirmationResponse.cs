using Bizca.User.Application.UseCases.RegisterCodeConfirmation;
using Newtonsoft.Json;

namespace Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode
{
    /// <summary>
    ///     Creates a code confirmation response.
    /// </summary>
    public sealed class RegisterCodeConfirmationResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="RegisterCodeConfirmationResponse"/>
        /// </summary>
        public RegisterCodeConfirmationResponse(RegisterCodeConfirmationDto confirmationCodeDto)
        {
            ChannelId = confirmationCodeDto.ChannelId;
            ChannelValue = confirmationCodeDto.ChannelValue;
            ConfirmationCode = confirmationCodeDto.ConfirmationCode;
        }

        /// <summary>
        ///     ChannelId ressource identifier.
        /// </summary>
        [JsonProperty("ressourceId")]
        public string ChannelId { get; }

        /// <summary>
        ///     ChannelId ressource value.
        /// </summary>
        [JsonProperty("ressource")]
        public string ChannelValue { get; }

        /// <summary>
        ///     ChannelId code confirmation.
        /// </summary>
        [JsonProperty("confirmationCode")]
        public string ConfirmationCode { get; }
    }
}