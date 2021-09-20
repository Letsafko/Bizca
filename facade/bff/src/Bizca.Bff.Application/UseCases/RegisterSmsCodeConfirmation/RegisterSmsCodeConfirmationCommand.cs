namespace Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Core.Application.Commands;
    public sealed class RegisterSmsCodeConfirmationCommand : ICommand
    {
        public RegisterSmsCodeConfirmationCommand(string partnerCode,
            string externalUserId,
            string phoneNumber)
        {
            ExternalUserId = externalUserId;
            PhoneNumber = phoneNumber;
            PartnerCode = partnerCode;
        }

        public ChannelType ChannelType = ChannelType.Sms;
        public string ExternalUserId { get; }
        public string PhoneNumber { get; }
        public string PartnerCode { get; }
    }
}
