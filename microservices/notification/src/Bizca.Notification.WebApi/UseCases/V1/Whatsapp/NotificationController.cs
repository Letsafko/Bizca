namespace Bizca.Notification.WebApi.UseCases.V1.Whatsapp
{
    using Core.Api.Modules.Conventions;
    using Core.Domain.Cqrs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    ///     Whatsapp notification controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/{partnerCode}/[controller]")]
    [ApiController]
    public sealed class NotificationController : ControllerBase
    {
        private readonly IProcessor _processor;

        /// <summary>
        ///     Creates an instance of <see cref="NotificationController" />
        /// </summary>
        /// <param name="processor"></param>
        public NotificationController(IProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        ///     Send whatsapp message.
        /// </summary>
        /// <param name="partnerCode">partner code identifier.</param>
        /// <param name="message">whatsapp message.</param>
        [HttpPost("whatsapp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WhatsappResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public async Task<IActionResult> SendWhatsappMessageAsync([Required] string partnerCode,
            WhatsappMessage message)
        {
            return Ok();
        }
    }
}