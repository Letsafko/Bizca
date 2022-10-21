namespace Bizca.Bff.Application.UseCases.ConfirmChannelCode
{
    using Core.Domain.Cqrs.Commands;

    public sealed class ConfirmChannelCodeCommand : ICommand
    {
        public ConfirmChannelCodeCommand(string channelType,
            string externalUserId,
            string confirmationCode,
            string partnerCode)
        {
            ConfirmationCode = confirmationCode;
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
            ChannelType = channelType;
        }

        public string ChannelType { get; }
        public string ConfirmationCode { get; }
        public string ExternalUserId { get; }
        public string PartnerCode { get; }
    }
}