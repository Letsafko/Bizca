namespace Bizca.Bff.WebApi.UseCases.V10.CreateNewUser
{
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
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
        ///     Creates a new user.
        /// </summary>
        /// <param name="input">channel confirmation code input.</param>
        /// <remarks>/Assets/createUser.md</remarks>
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> CreateUserAsync([Required][FromBody] CreateUser input)
        {
            CreateUserCommand command = GetCreateUserCommand(input);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return new OkObjectResult(true);
            //return presenter.ViewModel;
        }

        private CreateUserCommand GetCreateUserCommand(CreateUser input)
        {
            return new CreateUserCommand(input.ExternalUserId,
            input.PartnerCode,
            input.Civility,
            input.PhoneNumber,
            input.FirstName,
            input.LastName,
            input.Whatsapp,
            input.Email);
        }
    }
}
