namespace Bizca.Bff.WebApi.UseCases.V1._0.UpdateUser
{
    using Bizca.Bff.Application.UseCases.UpdateUser;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Bff.WebApi.ViewModels;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates user controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly UpdateUserPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public UsersController(UpdateUserPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Updates an user.
        /// </summary>
        /// <param name="externalUserId">user identifier.</param>
        /// <param name="user">user update informations.</param>
        /// <remarks>/Assets/updateUser.md</remarks>
        [HttpPut("{externalUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IPublicResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IPublicResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Update))]
        public async Task<IActionResult> UpdateUserAsync([Required] string externalUserId,
            [Required] [FromBody] UpdateUser user)
        {
            UpdateUserCommand command = GetUpdateUserCommand(externalUserId, user);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private UpdateUserCommand GetUpdateUserCommand(string externalUserId, UpdateUser user)
        {
            return new UpdateUserCommand(externalUserId,
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