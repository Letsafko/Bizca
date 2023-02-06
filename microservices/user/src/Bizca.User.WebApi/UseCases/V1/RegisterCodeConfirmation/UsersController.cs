namespace Bizca.User.WebApi.UseCases.V1.RegisterCodeConfirmation
{
    using Application.UseCases.RegisterCodeConfirmation;
    using Core.Api.Modules.Conventions;
    using Core.Domain.Cqrs;
    using Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates code confirmation controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly RegisterCodeConfirmationPresenter _presenter;
        private readonly IProcessor _processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="processor">command processor</param>
        /// <param name="presenter">register code confirmation presenter</param>
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
        /// <param name="request">register code confirmation request.</param>
        /// <remarks>/Assets/registerCodeConfirmation.md</remarks>
        [HttpPost("{externalUserId}/channel/code/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterCodeConfirmationResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> RegisterCodeConfirmationAsync([Required] string partnerCode,
            [Required] string externalUserId,
            [Required] [FromBody] RegisterCodeConfirmationRequest request)
        {
            var command = new RegisterCodeConfirmationCommand(partnerCode,
                externalUserId, 
                ChannelType.GetByLabel(request.Channel));
        
            await _processor.ProcessCommandAsync(command, CancellationToken.None);
            return _presenter.ViewModel;
        }
    }
}