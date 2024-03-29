﻿namespace Bizca.User.WebApi.UseCases.V1.CreateUser
{
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.Core.Domain;
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
        ///     Creates a new user.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="input">channel confirmation code input.</param>
        /// <remarks>/Assets/createUser.md</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IPublicResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(IPublicResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> CreateUserAsync([Required] string partnerCode, [Required][FromBody] CreateUser input)
        {
            CreateUserCommand command = GetCreateUserCommand(partnerCode, input);
            await _processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return _presenter.ViewModel;
        }

        private CreateUserCommand GetCreateUserCommand(string partnerCode, CreateUser input)
        {
            CreateUserCommandBuilder builder = CreateUserCommandBuilder.Instance
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
                .WithPhoneNumber(input.PhoneNumber);

            if (input.Address != null)
            {
                builder.WithAddress(input.Address?.Street, input.Address?.City, input.Address?.ZipCode, input.Address?.Country, input.Address?.Name);
            }
            return builder.Build();
        }
    }
}