namespace Bizca.Bff.WebApi.UseCases.V10.GetUsers
{
    using Bizca.Bff.Application.UseCases.GetUsers;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Get user detail controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly GetUsersPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            GetUsersPresenter presenter)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Retrieves list of users with pagination.
        /// </summary>
        /// <param name="criteria">user criteria search.</param>
        /// <remarks>/Assets/getUserByCriteria.md</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> GetUsers([Required][FromQuery] GetUsers criteria)
        {
            GetUsersQuery query = GetQuery(Resources.PartnerCode, criteria);
            await processor.ProcessQueryAsync(query).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private GetUsersQuery GetQuery(string partnerCode, GetUsers criteria)
        {
            return new GetUsersQuery(partnerCode,
                criteria.ExternalUserId,
                criteria.PhoneNumber,
                criteria.Email,
                criteria.FirstName,
                criteria.LastName);
        }
    }
}