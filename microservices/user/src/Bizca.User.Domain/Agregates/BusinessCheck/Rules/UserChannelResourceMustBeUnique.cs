namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Entities.Channel.Repositories;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class UserChannelResourceMustBeUnique : IUserRule
    {
        private readonly IChannelRepository channelRepository;

        public UserChannelResourceMustBeUnique(IChannelRepository channelRepository)
        {
            this.channelRepository = channelRepository;
        }

        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            Task<bool> isPhoneNumberExistsTask = IsChannelResourceExistsAsync(request.Partner.Id, request.PhoneNumber);
            Task<bool> isWhatsappExistsTask = IsChannelResourceExistsAsync(request.Partner.Id, request.Whatsapp);
            Task<bool> isEmailExistsTask = IsChannelResourceExistsAsync(request.Partner.Id, request.Email);
            await Task.WhenAll(isPhoneNumberExistsTask, isWhatsappExistsTask, isEmailExistsTask).ConfigureAwait(false);

            DomainFailure failure;
            if (isPhoneNumberExistsTask.Result)
            {
                failure = GetFailure(nameof(request.PhoneNumber), request.Partner.PartnerCode);
                return new RuleResult(false, failure);
            }

            if (isWhatsappExistsTask.Result)
            {
                failure = GetFailure(nameof(request.Whatsapp), request.Partner.PartnerCode);
                return new RuleResult(false, failure);
            }

            if (isEmailExistsTask.Result)
            {
                failure = GetFailure(nameof(request.Email), request.Partner.PartnerCode);
                return new RuleResult(false, failure);
            }

            return new RuleResult(true, null);
        }

        #region private helpers

        private Task<bool> IsChannelResourceExistsAsync(int partnerId, string channelResource)
        {
            Task<bool> isResourceUniqueTask = Task.FromResult(false);
            if (!string.IsNullOrWhiteSpace(channelResource))
                isResourceUniqueTask = channelRepository.IsExistAsync(partnerId, channelResource);
            return isResourceUniqueTask;
        }

        private DomainFailure GetFailure(string propertyName, string partnerCode)
        {
            return new DomainFailure($"{propertyName} must be unique for partner::{partnerCode}.",
                propertyName,
                typeof(ChannelResourceMustBeUniqueException));
        }

        #endregion
    }
}