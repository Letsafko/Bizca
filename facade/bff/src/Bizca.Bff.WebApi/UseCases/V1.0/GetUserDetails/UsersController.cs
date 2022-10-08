namespace Bizca.Bff.WebApi.UseCases.V10.GetUserDetails
{
    using Application.UseCases.GetUserDetails;
    using Core.Api.Modules.Conventions;
    using Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Properties;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ViewModels;

    /// <summary>
    ///     Get user detail controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly GetUserDetailsPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            GetUserDetailsPresenter presenter)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Retrieves user details.
        /// </summary>
        /// <param name="externalUserId">user identifier</param>
        /// <remarks>/Assets/getUserDetails.md</remarks>
        [HttpGet("{externalUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Get))]
        public async Task<IActionResult> GetUserDetailsAsync([Required] string externalUserId)
        {
            var query = new GetUserDetailsQuery(Resources.PartnerCode, externalUserId);
            await processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return presenter.ViewModel;
        }
    }
}