namespace Bizca.User.WebApi.UseCases.V1.GetUsers
{
    using Bizca.Core.Api.Modules.Common;
    using Bizca.Core.Application.Abstracts;
    using Bizca.User.Application.UseCases.GetUser.List;
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
        private readonly GetUsersPresenter _presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            GetUsersPresenter presenter)
        {
            _presenter = presenter;
            _processor = processor;
        }

        /// <summary>
        ///     Gets users.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="criteria">user criteria search.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> GetUsers([Required] string partnerCode, [Required][FromQuery] GetUsers criteria)
        {
            GetUsersQuery query = GetQuery(partnerCode, Request.Path, criteria);
            await _processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return _presenter.ViewModel;
        }

        private GetUsersQuery GetQuery(string partnerCode, string requestPath, GetUsers input)
        {
            return new GetUsersQuery(partnerCode, input.PageSize, input.PageNumber, input.Direction, input.BirthDate)
            {
                Email = input.Email,
                LastName = input.LastName,
                FirstName = input.FirstName,
                PhoneNumber = input.PhoneNumber,
                ExternalUserId = input.ExternalUserId,
                Whatsapp = input.Whatsapp,
                PageIndex = input.PageIndex,
                RequestPath = requestPath
            };
        }
    }
}
