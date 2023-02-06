namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Rules;
    using Entities.Channel.Repositories;
    using System.Threading.Tasks;

    public sealed class UserChannelResourceMustBeUnique : IUserRule
    {
        private readonly IChannelRepository _channelRepository;

        public UserChannelResourceMustBeUnique(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<CheckResult> CheckAsync(UserRequest request)
        {
            Task<bool> isPhoneNumberExistsTask = IsChannelResourceExistsAsync(request.Partner.PartnerId,
                request.PhoneNumber);

            Task<bool> isWhatsappExistsTask = IsChannelResourceExistsAsync(request.Partner.PartnerId,
                request.Whatsapp);

            Task<bool> isEmailExistsTask = IsChannelResourceExistsAsync(request.Partner.PartnerId,
                request.Email);

            await Task.WhenAll(isPhoneNumberExistsTask, isWhatsappExistsTask, isEmailExistsTask);

            CheckReport checkReport;
            if (isPhoneNumberExistsTask.Result)
            {
                checkReport = GetCheckReport(nameof(request.PhoneNumber), request.Partner.PartnerCode);
                return new CheckResult(false, checkReport);
            }

            if (isWhatsappExistsTask.Result)
            {
                checkReport = GetCheckReport(nameof(request.Whatsapp), request.Partner.PartnerCode);
                return new CheckResult(false, checkReport);
            }

            if (!isEmailExistsTask.Result) return new CheckResult(true, null);

            checkReport = GetCheckReport(nameof(request.Email), request.Partner.PartnerCode);
            return new CheckResult(false, checkReport);
        }

        private Task<bool> IsChannelResourceExistsAsync(int partnerId, string channelResource)
        {
            Task<bool> isResourceUniqueTask = Task.FromResult(false);

            if (!string.IsNullOrWhiteSpace(channelResource))
                isResourceUniqueTask = _channelRepository.IsExistAsync(partnerId, channelResource);

            return isResourceUniqueTask;
        }

        private static CheckReport GetCheckReport(string propertyName, string partnerCode)
        {
            return new CheckReport($"{propertyName} must be unique for partner::{partnerCode}.",
                propertyName);
        }
    }
}