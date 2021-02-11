namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    public sealed class RegisterCodeConfirmationDto
    {
        public RegisterCodeConfirmationDto(string channelId, string channelValue, string codeConfirmation)
        {
            ChannelId = channelId;
            ChannelValue = channelValue;
            ConfirmationCode = codeConfirmation;
        }

        public string ChannelId { get; }
        public string ChannelValue { get; }
        public string ConfirmationCode { get; }
    }
}