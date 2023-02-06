namespace Bizca.Bff.Application.UseCases.ConfirmChannelCode
{
    using Core.Domain;

    public interface IConfirmChannelCodeOutput : IPublicErrorOutput
    {
        void Ok(string channelType, string channelValue, bool confirmed);
    }
}