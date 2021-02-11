namespace Bizca.User.WebApi.UseCases.V1.GetUserDetails
{
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Get user detail controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IProcessor _processor;
        private readonly GetUserDetailPresenter _presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            GetUserDetailPresenter presenter)
        {
            _presenter = presenter;
            _processor = processor;
        }

        /// <summary>
        ///     Gets user detail informations.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="userId">external user identitfier.</param>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDetailResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
        public async Task<IActionResult> GetUserDetails([Required] string partnerCode, [Required] string userId)
        {
            var query = new GetUserDetailQuery(userId, partnerCode);
            await _processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return _presenter.ViewModel;
        }
    }
}