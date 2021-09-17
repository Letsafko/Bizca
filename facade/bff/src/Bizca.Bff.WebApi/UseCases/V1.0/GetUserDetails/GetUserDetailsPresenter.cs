namespace Bizca.Bff.WebApi.UseCases.V10.GetUserDetails
{
    using Bizca.Bff.Application.UseCases.GetUserDetails;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Presentation;
    using Bizca.Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Get user details preseneter.
    /// </summary>
    public sealed class GetUserDetailsPresenter : PresenterBase, IGetUserDetailsOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="GetUserDetailsPresenter"/>
        /// </summary>
        /// <param name="strategyFactory"></param>
        public GetUserDetailsPresenter(IHttpStrategyFactory strategyFactory) : base(strategyFactory)
        {
        }

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="detailsDto"></param>
        public void Ok(GetUserDetailsDto detailsDto)
        {
            ViewModel = new OkObjectResult(new UserViewModel(detailsDto));
        }
    }
}
