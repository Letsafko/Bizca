namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserWhatsappMandatory : IUserRule
    {
        public Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.Whatsapp & request.Partner.MandatoryUserProfileField) == 0 ||
                !string.IsNullOrWhiteSpace(request.Whatsapp);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("whatsapp number is mandatory.", nameof(request.Whatsapp));

            return Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}