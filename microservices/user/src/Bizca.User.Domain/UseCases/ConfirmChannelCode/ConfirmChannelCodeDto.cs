namespace Bizca.User.Application.UseCases.ConfirmChannelCode;

using Domain;

public sealed class ConfirmChannelCodeDto
{
    public ConfirmChannelCodeDto(ChannelType channelType, string channelValue, bool confirmed)
    {
        ChannelType = channelType;
        ChannelValue = channelValue;
        ChannelConfirmed = confirmed;
    }

    public ChannelType ChannelType { get; }
    public bool ChannelConfirmed { get; }
    public string ChannelValue { get; }
}