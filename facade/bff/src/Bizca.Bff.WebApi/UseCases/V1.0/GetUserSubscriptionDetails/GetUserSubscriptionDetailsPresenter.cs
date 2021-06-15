namespace Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptionDetails
{
    using Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     User subscription details presenter.
    /// </summary>
    public sealed class GetUserSubscriptionDetailsPresenter : IGetUserSubscriptionDetailsOutput
    {
        /// <summary>
        ///     User subscription details view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="subscription"></param>
        public void Ok(Subscription subscription)
        {
            ViewModel = new OkObjectResult(new GetUserSubscriptionDetailsResponse(subscription));
        }
    }
}