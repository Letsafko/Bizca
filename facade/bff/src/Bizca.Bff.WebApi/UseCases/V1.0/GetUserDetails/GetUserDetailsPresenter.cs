namespace Bizca.Bff.WebApi.UseCases.V10.GetUserDetails
{
    using Application.UseCases.GetUserDetails;
    using Core.Api.Modules.Presentation;
    using Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Get user details preseneter.
    /// </summary>
    public sealed class GetUserDetailsPresenter : PresenterBase, IGetUserDetailsOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="GetUserDetailsPresenter" />
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