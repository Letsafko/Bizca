﻿namespace Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode
{
    using Application.UseCases.ConfirmChannelCode;
    using Core.Api.Modules.Conventions;
    using Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Creates code confirmation controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly ConfirmChannelCodePresenter _presenter;
        private readonly IProcessor _processor;

        /// <summary>
        ///     Create an instance of <see cref="UsersController" />
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="presenter"></param>
        public UsersController(IProcessor processor,
            ConfirmChannelCodePresenter presenter)
        {
            _presenter = presenter;
            _processor = processor;
        }

        /// <summary>
        ///     Validates a channel code confirmation.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="externalUserId">user identifier of partner.</param>
        /// <param name="input">register code confirmation input.</param>
        /// <remarks>/Assets/confirmChannelCode.md</remarks>
        [HttpPost("{externalUserId}/channel/code/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfirmChannelCodeResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Update))]
        public async Task<IActionResult> RegisterCodeConfirmationAsync([Required] string partnerCode,
            [Required] string externalUserId,
            [Required] [FromBody] ConfirmChannelCode input)
        {
            ChannelConfirmationCommand command = GetConfirmationCommand(externalUserId, partnerCode, input);
            await _processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return _presenter.ViewModel;
        }

        private ChannelConfirmationCommand GetConfirmationCommand(string userId, string partnerCode,
            ConfirmChannelCode input)
        {
            return ConfirmChannelCodeCommandBuilder.Instance
                .WithChannel(input.Channel)
                .WithExternalUserId(userId)
                .WithPartnerCode(partnerCode)
                .WithConfirmationCode(input.CodeConfirmation)
                .Build();
        }
    }
}