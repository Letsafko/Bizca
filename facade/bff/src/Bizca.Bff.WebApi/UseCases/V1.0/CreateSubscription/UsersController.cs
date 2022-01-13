namespace Bizca.Bff.WebApi.UseCases.V10.CreateSubscription
{
    using Bizca.Bff.Application.UseCases.CreateSubscription;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.Core.Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates subscription controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Subscriptions")]
    public sealed class UsersController : ControllerBase
    {
        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(CreateSubscriptionPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        private readonly CreateSubscriptionPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Creates a new subscription for an user.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <param name="subscription">subscription.</param>
        /// <remarks>/Assets/createSubscription.md</remarks>
        [HttpPost("{externalUserId}/subscriptions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IPublicResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> CreateSubscriptionAsync([Required] string externalUserId,
            [Required][FromBody] CreateSubscription subscription)
        {
            CreateSubscriptionCommand command = GetCreateSubscriptionCommand(externalUserId, subscription);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private CreateSubscriptionCommand GetCreateSubscriptionCommand(string externalUserId, CreateSubscription subscription)
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