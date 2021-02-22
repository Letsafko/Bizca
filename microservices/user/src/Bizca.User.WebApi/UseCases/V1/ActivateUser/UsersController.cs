namespace Bizca.User.WebApi.UseCases.V1.ActivateUser
{
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
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
        private readonly IProcessor processor;
        private readonly ActivateUserPresenter presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            ActivateUserPresenter presenter)
        {
            this.presenter = presenter;
            this.processor = processor;
        }

        /// <summary>
        ///     Activates or desactivates an user.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="externalUserId">user identifier of partner.</param>
        /// <param name="input">activate user.</param>
        /// <remarks>/Assets/activateUser.md</remarks>
        [HttpPost("{externalUserId}/activate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActivateUserResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Update))]
        public async Task<IActionResult> RegisterCodeConfirmationAsync([Required] string partnerCode,
            [Required] string externalUserId,
            [Required][FromBody] ActivateUser input)
        {
            return presenter.ViewModel;
        }
    }
}