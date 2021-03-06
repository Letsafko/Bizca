namespace Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode
{
    using Bizca.User.Application.UseCases.RegisterCodeConfirmation;

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
            ConfirmationCode = confirmationCodeDto.ConfirmationCode;
            Ressource = confirmationCodeDto.ChannelValue;
            ResourceId = confirmationCodeDto.ChannelId;
        }

        /// <summary>
        ///     ChannelId code confirmation.
        /// </summary>
        public string ConfirmationCode { get; }

        /// <summary>
        ///     ChannelId ressource identifier.
        /// </summary>
        public string ResourceId { get; }

        /// <summary>
        ///     ChannelId ressource value.
        /// </summary>
        public string Ressource { get; }
    }
}