namespace Bizca.User.WebApi.UseCases.V1.RegisterCodeConfirmation
{
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.User.Application.UseCases.RegisterCodeConfirmation;
    using Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates code confirmation controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IProcessor _processor;
        private readonly RegisterCodeConfirmationPresenter _presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            RegisterCodeConfirmationPresenter presenter)
        {
            _presenter = presenter;
            _processor = processor;
        }

        /// <summary>
        ///     Creates a new channel code confirmation.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="externalUserId">user identifier of partner.</param>
        /// <param name="input">register code confirmation input.</param>
        /// <remarks>/Assets/registerCodeConfirmation.md</remarks>
        [HttpPost("{externalUserId}/channel/code/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterCodeConfirmationResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> RegisterCodeConfirmationAsync([Required] string partnerCode,
            [Required] string externalUserId,
            [Required][FromBody] RegisterCodeConfirmation input)
        {
            RegisterCodeConfirmationCommand command = GetConfirmationCommand(externalUserId, partnerCode, input);
            await _processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return _presenter.ViewModel;
        }

        private RegisterCodeConfirmationCommand GetConfirmationCommand(string userId, string partnerCode, RegisterCodeConfirmation input)
        {
            return RegisterCodeConfirmationBuilder.Instance
                .WithChannel(input.Channel)
                .WithExternalUserId(userId)
                .WithPartnerCode(partnerCode)
                .Build();
        }
    }
}