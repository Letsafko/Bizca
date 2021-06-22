using Bizca.Bff.Domain.Properties;

namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    public sealed class UserConfirmationCodeRequest
    {
        public UserConfirmationCodeRequest(string codeConfirmation, string channel)
        {
            CodeConfirmation = codeConfirmation;
            Channel = channel;
        }

        public string PartnerCode { get; } = Resources.PartnerCode;
        public string CodeConfirmation { get; }
        public string Channel { get; }
    }
}
