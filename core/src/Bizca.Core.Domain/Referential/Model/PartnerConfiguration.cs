namespace Bizca.Core.Domain.Referential.Model
{
    using Enums;

    public sealed class PartnerConfiguration
    {
        public PartnerConfiguration(MandatoryUserProfileField mandatoryUserProfileField = MandatoryUserProfileField.Email, 
            MandatoryAddressField mandatoryAddressField = MandatoryAddressField.None, 
            int channelCodeConfirmationExpirationDelay = 24 * 60, 
            int channelCodeConfirmationLength = 10)
        {
            ChannelCodeConfirmationExpirationDelay = channelCodeConfirmationExpirationDelay;
            ChannelCodeConfirmationLength = channelCodeConfirmationLength;
            MandatoryUserProfileField = mandatoryUserProfileField;
            MandatoryAddressField = mandatoryAddressField;
        }

        public MandatoryUserProfileField MandatoryUserProfileField { get; }
        public MandatoryAddressField MandatoryAddressField { get; }
        public int ChannelCodeConfirmationExpirationDelay { get; }
        public int ChannelCodeConfirmationLength { get; }
    }
}