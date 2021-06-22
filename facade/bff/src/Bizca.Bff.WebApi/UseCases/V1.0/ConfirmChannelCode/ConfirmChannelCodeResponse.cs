namespace Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode
{
    using Bizca.Bff.WebApi.ViewModels;

    /// <summary>
    ///     Confirmation code response.
    /// </summary>
    public sealed class ConfirmChannelCodeResponse : ConfirmationCodeViewModel
    {
        /// <summary>
        ///     Create an instance of <see cref="ConfirmChannelCodeResponse"/>
        /// </summary>
        public ConfirmChannelCodeResponse(string channelType, string channelValue, bool confirmed)
            : base(channelType, channelValue, confirmed)
        {
        }
    }
}
