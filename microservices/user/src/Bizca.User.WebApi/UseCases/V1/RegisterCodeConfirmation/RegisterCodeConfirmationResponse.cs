namespace Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode
{
    using Application.UseCases.RegisterCodeConfirmation;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Creates a code confirmation response.
    /// </summary>
    public sealed class RegisterCodeConfirmationResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="RegisterCodeConfirmationResponse" />
        /// </summary>
        public RegisterCodeConfirmationResponse(RegisterCodeConfirmationDto confirmationCodeDto)
        {
            ConfirmationCode = confirmationCodeDto.ConfirmationCode;
            Resource = confirmationCodeDto.ChannelValue;
            ResourceId = confirmationCodeDto.ChannelId;
        }

        /// <summary>
        ///     ChannelId code confirmation.
        /// </summary>
        [Required]
        public string ConfirmationCode { get; }

        /// <summary>
        ///     ChannelId ressource identifier.
        /// </summary>
        [Required]
        public string ResourceId { get; }

        /// <summary>
        ///     ChannelId ressource value.
        /// </summary>
        [Required]
        public string Resource { get; }
    }
}