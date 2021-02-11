namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
    using Bizca.User.Application.UseCases.ConfirmChannelCode;
    using Newtonsoft.Json;

    /// <summary>
    ///     Confirmation channel code response.
    /// </summary>
    public sealed class ConfirmChannelCodeResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="ConfirmChannelCodeResponse"/>
        /// </summary>
        public ConfirmChannelCodeResponse(ConfirmChannelCodeDto confirmationCodeDto)
        {
            ChannelId = confirmationCodeDto.ChannelType.Code.ToLower();
            ChannelValue = confirmationCodeDto.ChannelValue;
            Confirmed = confirmationCodeDto.ChannelConfirmed;
        }

        /// <summary>
        ///     Channel ressource identifier.
        /// </summary>
        [JsonProperty("ressourceId")]
        public string ChannelId { get; }

        /// <summary>
        ///     Channel ressource value.
        /// </summary>
        [JsonProperty("ressource")]
        public string ChannelValue { get; }

        /// <summary>
        ///     Channel code confirmed.
        /// </summary>
        public bool Confirmed { get; }
    }
}