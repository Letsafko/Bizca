namespace Bizca.User.WebApi.UseCases.V1.GetUsersByCriteria
{
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.User.Application.UseCases.GetUsersByCriteria;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///     Get user detail controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        public UsersController(IProcessor processor)
        {
            this.processor = processor;
        }

        /// <summary>
        ///     Retrieves list of users with pagination.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="criteria">user criteria search.</param>
        /// <remarks>/Assets/getUserByCriteria.md</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.List))]
        public async Task<IActionResult> GetUsers([Required] string partnerCode, [Required][FromQuery] GetUsersByCriteria criteria)
        {
            GetUsersQuery query = GetQuery(partnerCode, criteria);
            IEnumerable<GetUsers> users = await processor.ProcessQueryAsync(query).ConfigureAwait(false);
            var response = new GetUsersResponse(users?.ToList(), criteria, HttpContext.Request.Path);
            return Ok(response);
        }

        private GetUsersQuery GetQuery(string partnerCode, GetUsersByCriteria input)
        {
            return new GetUsersQuery
            {
                PartnerCode = partnerCode,
                ExternalUserId = input.ExternalUserId,
                Email = input.Email,
                LastName = input.LastName,
                FirstName = input.FirstName,
                PhoneNumber = input.PhoneNumber,
                Whatsapp = input.Whatsapp,
                BirthDate = input.BirthDate,
                PageSize = input.PageSize,
                PageIndex = input.PageIndex,
                Direction = input.Direction
            };
        }
    }
}