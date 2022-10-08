namespace Bizca.Bff.WebApi.UseCases.V10.AuthenticateUser
{
    using Application.UseCases.AuthenticateUser;
    using Core.Api.Modules.Conventions;
    using Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Authenticate user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly AuthenticateUserPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(AuthenticateUserPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Authenticate user.
        /// </summary>
        /// <param name="authenticateUser">authenticate user</param>
        /// <remarks>/Assets/authenticateUser.md</remarks>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
        public async Task<IActionResult> AuthenticateUserAsync([Required] [FromBody] AuthenticateUser authenticateUser)
        {
            var query = new AuthenticateUserQuery(authenticateUser.Password, authenticateUser.Resource);
            await processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}