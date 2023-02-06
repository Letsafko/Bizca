namespace Bizca.Core.Domain.Referential.Model
{
    using Enums;
    using System.Collections.Generic;

    public sealed class Partner : ValueObject
    {
        private readonly PartnerConfiguration _partnerConfiguration;

        public Partner(int partnerId, string code, string description, PartnerConfiguration partnerConfiguration = null)
        {
            _partnerConfiguration = partnerConfiguration ?? new PartnerConfiguration();
            Description = description;
            PartnerId = partnerId;
            PartnerCode = code;
        }

        public int ChannelCodeConfirmationExpirationDelay => _partnerConfiguration.ChannelCodeConfirmationExpirationDelay;
        public MandatoryUserProfileField MandatoryUserProfileField => _partnerConfiguration.MandatoryUserProfileField;
        public MandatoryAddressField MandatoryAddressField => _partnerConfiguration.MandatoryAddressField;
        public int ChannelCodeConfirmationLength => _partnerConfiguration.ChannelCodeConfirmationLength;
        public string PartnerCode { get; }
        public string Description { get; }
        public int PartnerId { get; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PartnerCode;
            yield return PartnerId;
        }
    }
}