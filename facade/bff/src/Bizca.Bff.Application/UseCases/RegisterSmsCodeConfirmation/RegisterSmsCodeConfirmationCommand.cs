namespace Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation
{
    using Core.Application.Commands;
    using Domain.Enumerations;

    public sealed class RegisterSmsCodeConfirmationCommand : ICommand
    {
        public ChannelType ChannelType = ChannelType.Sms;

        public RegisterSmsCodeConfirmationCommand(string partnerCode,
            string externalUserId,
            string phoneNumber)
        {
            ExternalUserId = externalUserId;
            PhoneNumber = phoneNumber;
            PartnerCode = partnerCode;
        }

        public string ExternalUserId { get; }
        public string PhoneNumber { get; }
        public string PartnerCode { get; }
    }
}