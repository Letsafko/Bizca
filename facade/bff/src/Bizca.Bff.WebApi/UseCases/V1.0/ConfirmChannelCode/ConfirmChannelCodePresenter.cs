namespace Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode
{
    using Bizca.Bff.Application.UseCases.ConfirmChannelCode;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Presentation;
    using Bizca.Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Confirmation channel code presenter.
    /// </summary>
    public sealed class ConfirmChannelCodePresenter : PresenterBase, IConfirmChannelCodeOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="ConfirmChannelCodePresenter"/>
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