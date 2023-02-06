namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    using Properties;

    public sealed class UserConfirmationCodeRequest
    {
        public UserConfirmationCodeRequest(string externalUserId,
            string codeConfirmation,
            string channel)
        {
            CodeConfirmation = codeConfirmation;
            ExternalUserId = externalUserId;
            Channel = channel;
        }

        public string PartnerCode { get; } = Resources.PartnerCode;
        public string CodeConfirmation { get; }
        public string ExternalUserId { get; }
        public string Channel { get; }
    }
}