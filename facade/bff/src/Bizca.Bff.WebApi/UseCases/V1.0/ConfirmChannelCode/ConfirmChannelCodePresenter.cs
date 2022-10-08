namespace Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode
{
    using Application.UseCases.ConfirmChannelCode;
    using Core.Api.Modules.Presentation;
    using Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Confirmation channel code presenter.
    /// </summary>
    public sealed class ConfirmChannelCodePresenter : PresenterBase, IConfirmChannelCodeOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="ConfirmChannelCodePresenter" />
        /// </summary>
        /// <param name="strategyFactory"></param>
        public ConfirmChannelCodePresenter(IHttpStrategyFactory strategyFactory) : base(strategyFactory)
        {
        }

        /// <summary>
        ///     Standard output.
        /// </summary>
        public void Ok(string channelType, string channelValue, bool confirmed)
        {
            ViewModel = new OkObjectResult(new ConfirmationCodeViewModel(channelType, channelValue, confirmed));
        }
    }
}