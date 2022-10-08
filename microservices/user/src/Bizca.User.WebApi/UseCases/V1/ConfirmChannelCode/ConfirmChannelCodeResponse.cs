namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
    using Application.UseCases.ConfirmChannelCode;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Confirmation channel code response.
    /// </summary>
    public sealed class ConfirmChannelCodeResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="ConfirmChannelCodeResponse" />
        /// </summary>
        public ConfirmChannelCodeResponse(ConfirmChannelCodeDto confirmationCodeDto)
        {
            ResourceId = confirmationCodeDto.ChannelType.Description.ToLower();
            Confirmed = confirmationCodeDto.ChannelConfirmed;
            Resource = confirmationCodeDto.ChannelValue;
        }

        /// <summary>
        ///     Channel ressource identifier.
        /// </summary>
        [Required]
        public string ResourceId { get; }

        /// <summary>
        ///     Channel ressource value.
        /// </summary>
        [Required]
        public string Resource { get; }

        /// <summary>
        ///     Channel code confirmed.
        /// </summary>
        [Required]
        public bool Confirmed { get; }
    }
}