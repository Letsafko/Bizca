namespace Bizca.Bff.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Core.Domain;
    public interface IConfirmChannelCodeOutput : IPublicErrorOutput
    {
        void Ok(string channelType, string channelValue, bool confirmed);
    }
}