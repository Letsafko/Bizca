namespace Bizca.Bff.WebApi.UseCases.V10.UpdateSubscription
{
    using Application.UseCases.UpdateSubscription;
    using Core.Api.Modules.Conventions;
    using Core.Application;
    using Core.Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Creates subscription controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Subscriptions")]
    public sealed class UsersController : ControllerBase
    {
        private readonly UpdateSubscriptionPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(UpdateSubscriptionPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Updates subscription of an user.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="reference">subscription code identifier.</param>
        /// <param name="subscription">subscription to update.</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpPut("{externalUserId}/subscriptions/{reference}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IPublicResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IPublicResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Update))]
        public async Task<IActionResult> UpdateSubscriptionAsync([Required] string externalUserId,
            [Required] string reference,
            [Required] [FromBody] UpdateSubscription subscription)
        {
            UpdateSubscriptionCommand command = GetUpdateSubscriptionCommand(externalUserId, reference, subscription);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private UpdateSubscriptionCommand GetUpdateSubscriptionCommand(string externalUserId,
            string subscriptionCode,
            UpdateSubscription subscription)
        {
            return new UpdateSubscriptionCommand(externalUserId,
                subscriptionCode,
                subscription.CodeInsee,
                subscription.ProcedureTypeId);
        }
    }
}