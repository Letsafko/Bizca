namespace Bizca.User.WebApi.UseCases.V1.GetUser
{
    using Bizca.Core.Application.Abstracts;
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.WebApi.Modules;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IProcessor _processor;
        private readonly GetUserDetailPresenter _presenter;
        public UsersController(IProcessor processor,
            GetUserDetailPresenter presenter)
        {
            _presenter = presenter;
            _processor = processor;
        }

        [HttpGet("{userId}", Name = "GetUser")]
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
