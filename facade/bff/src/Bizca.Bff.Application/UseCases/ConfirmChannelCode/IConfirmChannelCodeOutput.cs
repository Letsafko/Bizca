namespace Bizca.Bff.Application.UseCases.ConfirmChannelCode
{
    public interface IConfirmChannelCodeOutput
    {
        void Ok(string channelType, string channelValue, bool confirmed);
    }
}
