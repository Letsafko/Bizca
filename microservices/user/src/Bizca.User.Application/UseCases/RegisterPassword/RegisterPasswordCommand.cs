namespace Bizca.User.Application.UseCases.RegisterPassword
{
    using Bizca.Core.Application.Commands;

    public sealed class RegisterPasswordCommand : ICommand
    {
        public string PartnerCode { get; }
        public string ExternalUserId { get; }
        public string Password { get; }
        public RegisterPasswordCommand(string partnerCode, string externalUserId, string password)
        {
            Password = password;
            PartnerCode = partnerCode;
            ExternalUserId = externalUserId;
        }
    }
}
