namespace Bizca.Core.Integration.Test.Repository
{
    using Core.Test.Support;
    using Dapper;
    using Domain.Referential.Model;
    using Domain.Referential.Repository;
    using System.Data;
    using Xunit;
    using static Bizca.Core.Test.Support.Builder.PartnerExtensions;

    [Trait("Category", "IntegrationTest")]
    public class PartnerRepositoryTest: IntegrationTestBase<CoreStartup>
    {
        private readonly IPartnerRepository _partnerRepository;
        public PartnerRepositoryTest(CustomAutofacWebApplicationFactory<CoreStartup> factory): base(factory)
        {
            _partnerRepository = GetService<IPartnerRepository>();
        }

        private readonly Partner _partner = BuildNewPartner();

        public override async Task InitializeAsync()
        {
            UnitOfWork.Begin();
            await UnitOfWork
                .Connection
                .ExecuteAsync(
                    $@"insert into ref.partner
                    (
                        partnerId,
                        partnerCode,
                        description,
	                    confirmation_code_delay_in_minutes,
	                    confirmation_code_length,
	                    mandatory_address_field_mask,
	                    mandatory_user_profile_field_mask
                    )
                    values
                    (
                        @partnerId,
                        @partnerCode,
                        @description,
	                    @confirmationCodeDelay,
	                    @confirmationCodeLength,
	                    @mandatoryAddressFieldMask,
	                    @mandatoryUserProfileFieldMask
                    )",
                    param: new
                    {
                        @partnerId = _partner.PartnerId,
                        @partnerCode = _partner.PartnerCode,
                        @description = _partner.Description,
                        @mandatoryAddressFieldMask = _partner.MandatoryAddressField,
                        @confirmationCodeLength = _partner.ChannelCodeConfirmationLength,
                        @mandatoryUserProfileFieldMask = _partner.MandatoryUserProfileField,
                        @confirmationCodeDelay = _partner.ChannelCodeConfirmationExpirationDelay
                    },
                    commandType: CommandType.Text,
                    transaction: UnitOfWork.Transaction);
        }

        [Fact]
        public async Task Should_retrieve_partner()
        {
            //act
            var partner = await _partnerRepository.GetByCodeAsync(_partner.PartnerCode);

            //assert
            Assert.NotNull(partner);
        
            Assert.Equal(_partner.ChannelCodeConfirmationExpirationDelay, 
                partner.ChannelCodeConfirmationExpirationDelay);
        
            Assert.Equal(_partner.ChannelCodeConfirmationLength, 
                partner.ChannelCodeConfirmationLength);
        
            Assert.Equal(_partner.MandatoryUserProfileField, 
                partner.MandatoryUserProfileField);
        
            Assert.Equal(_partner.MandatoryAddressField, partner.MandatoryAddressField);
        
            Assert.Equal(_partner.Description, partner.Description);
        
            Assert.Equal(_partner.PartnerCode, partner.PartnerCode);
        
            Assert.Equal(_partner.PartnerId, partner.PartnerId);
        }

    }
}