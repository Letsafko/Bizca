namespace Bizca.User.WebApi.UseCases.V1.GetUserDetails
{
    using Application.UseCases.GetUserDetail;
    using Core.Api.Modules.Conventions;
    using Core.Domain.Cqrs;
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
        private readonly GetUserDetailPresenter _presenter;
        private readonly IProcessor _processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
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
        ///     Retrieves user details.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="externalUserId">user identitfier of partner.</param>
        /// <remarks>/Assets/getUserDetails.md</remarks>
        [HttpGet("{externalUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDetailResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
        public async Task<IActionResult> GetUserDetails([Required] string partnerCode, [Required] string externalUserId)
        {
            var query = new GetUserDetailQuery(externalUserId, partnerCode);
            await _processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return _presenter.ViewModel;
        }
    }
}