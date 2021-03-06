namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
    using Bizca.User.Application.UseCases.ConfirmChannelCode;

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
            ResourceId = confirmationCodeDto.ChannelType.Code.ToLower();
            Confirmed = confirmationCodeDto.ChannelConfirmed;
            Ressource = confirmationCodeDto.ChannelValue;
        }

        /// <summary>
        ///     Channel ressource identifier.
        /// </summary>
        public string ResourceId { get; }

        /// <summary>
        ///     Channel ressource value.
        /// </summary>
        public string Ressource { get; }

        /// <summary>
        ///     Channel code confirmed.
        /// </summary>
        public bool Confirmed { get; }
    }
}