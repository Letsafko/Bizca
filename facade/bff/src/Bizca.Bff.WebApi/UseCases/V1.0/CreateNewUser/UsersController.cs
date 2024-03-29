﻿namespace Bizca.Bff.WebApi.UseCases.V10.CreateNewUser
{
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Bizca.Core.Domain;
    using Microsoft.AspNetCore.Http;
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
        private readonly CreateNewUserPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(CreateNewUserPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Creates a new user.
        /// </summary>
        /// <param name="user">channel confirmation code input.</param>
        /// <remarks>/Assets/createUser.md</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IPublicResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> CreateUserAsync([Required][FromBody] CreateUser user)
        {
            CreateUserCommand command = GetCreateUserCommand(user);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private CreateUserCommand GetCreateUserCommand(CreateUser user)
        {
            return new CreateUserCommand(user.ExternalUserId,
                Resources.PartnerCode,
                user.Civility,
                user.PhoneNumber,
                user.FirstName,
                user.LastName,
                null,
                user.Email);
        }
    }
}