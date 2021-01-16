namespace Bizca.User.WebApi.UseCases.V1.CreateUser
{
    using Bizca.Core.Api.Modules.Common;
    using Bizca.Core.Application.Abstracts;
    using Bizca.User.Application.UseCases.CreateUser;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IProcessor _processor;
        private readonly CreateUserPresenter _presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            CreateUserPresenter presenter)
        {
            _presenter = presenter;
            _processor = processor;
        }

        /// <summary>
        ///     Create user.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="input">channel confirmation code input.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> CreateUserAsync([Required] string partnerCode, [Required][FromBody] CreateUser input)
        {
            CreateUserCommand command = GetCreateUserCommand(partnerCode, input);
            await _processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return _presenter.ViewModel;
        }

        private CreateUserCommand GetCreateUserCommand(string partnerCode, CreateUser input)
        {
            return CreateUserCommandBuilder.Instance
                .WithPartnerCode(partnerCode)
                .WithExternalUserId(input.ExternalUserId)
                .WithEconomicActivity(input.EconomicActivity)
                .WithCivility(input.Civility)
                .WithBirthDate(input.BirthDate)
                .WithEmail(input.Email)
                .WithBirthCity(input.BirthCity)
                .WithBirthCountry(input.BirthCountry)
                .WithLastName(input.LastName)
                .WithFirstName(input.FirstName)
                .WithWhatsapp(input.Whatsapp)
                .WithPhoneNumber(input.PhoneNumber)
                .Build();
        }
    }
}
