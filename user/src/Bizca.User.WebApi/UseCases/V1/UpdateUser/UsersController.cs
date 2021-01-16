namespace Bizca.User.WebApi.UseCases.V1.UpdateUser
{
    using Bizca.Core.Api.Modules.Common;
    using Bizca.Core.Application.Abstracts;
    using Bizca.User.Application.UseCases.UpdateUser;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Update user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly IProcessor _processor;
        private readonly UpdateUserPresenter _presenter;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            UpdateUserPresenter presenter)
        {
            _presenter = presenter;
            _processor = processor;
        }

        /// <summary>
        ///     Update user informations.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="userId"> user identifier.</param>
        /// <param name="input">update user input.</param>
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Update))]
        public async Task<IActionResult> UpdateUser([Required] string partnerCode, [Required] string userId, [Required][FromBody] UpdateUser input)
        {
            UpdateUserCommand command = GetUpdateUserCommand(partnerCode, userId, input);
            await _processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return _presenter.ViewModel;
        }

        private UpdateUserCommand GetUpdateUserCommand(string partnerCode, string externalUserId, UpdateUser input)
        {
            return UpdateUserCommandBuilder.Instance
                .WithPartnerCode(partnerCode)
                .WithExternalUserId(externalUserId)
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
