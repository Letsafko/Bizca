namespace Bizca.Bff.WebApi.UseCases.V10.CreateSubscription
{
    using Application.UseCases.CreateSubscription;
    using Core.Api.Modules.Conventions;
    using Core.Application;
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
        private readonly CreateSubscriptionPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(CreateSubscriptionPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Creates a new subscription for an user.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="subscription">subscription.</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpPost("{externalUserId}/subscriptions")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubscriptionViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> CreateSubscriptionAsync([Required] string externalUserId,
            [Required] [FromBody] CreateSubscription subscription)
        {
            CreateSubscriptionCommand command = GetCreateSubscriptionCommand(externalUserId, subscription);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private CreateSubscriptionCommand GetCreateSubscriptionCommand(string externalUserId,
            CreateSubscription subscription)
        {
            return new CreateSubscriptionCommand(externalUserId,
                subscription.CodeInsee,
                subscription.ProcedureTypeId,
                subscription.FirstName,
                subscription.LastName,
                subscription.PhoneNumber,
                null,
                subscription.Email);
        }
    }
}