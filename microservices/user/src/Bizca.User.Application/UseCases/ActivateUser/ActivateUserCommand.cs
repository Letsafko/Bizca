namespace Bizca.User.Application.UseCases.ActivateUser
{
    using Bizca.Core.Application.Commands;

    public sealed class ActivateUserCommand : ICommand
    {
        public string Activate { get; }
        public string PartnerCode { get; }
        public string ExternalUserId { get; }
        public ActivateUserCommand(string partnerCode, string externalUserId, string activate)
        {
            Activate = activate;
            PartnerCode = partnerCode;
            ExternalUserId = externalUserId;
        }
    }
}
