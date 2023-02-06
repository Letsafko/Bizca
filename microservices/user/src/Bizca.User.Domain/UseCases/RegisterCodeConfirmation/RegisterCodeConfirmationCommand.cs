namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation;

using Core.Domain.Cqrs.Commands;
using Domain;

public sealed class RegisterCodeConfirmationCommand : ICommand
{
    public RegisterCodeConfirmationCommand(string partnerCode, string externalUserId, ChannelType channelType)
    {
        PartnerCode = partnerCode;
        ExternalUserId = externalUserId;
        ChannelType = channelType;
    }

    public ChannelType ChannelType { get; }
    public string ExternalUserId { get; }
    public string PartnerCode { get; }
}