﻿namespace Bizca.Bff.WebApi.UseCases.V10.SendProcedureAppointmentAvailability
{
    using Bizca.Bff.Application.UseCases.SendAppointmentAvailability;
    using Bizca.Bff.WebApi.Properties;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Application;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    ///     Send appointment availability of a procedure to subscribers controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:api-version}/[controller]")]
    [ApiController]
    public sealed class AvailabiltyController : ControllerBase
    {
        private readonly SendProcedureAppointmentAvailabilityPresenter presenter;
        private readonly IProcessor processor;

        /// <summary>
        ///     Create an instance of <see cref="AvailabiltyController"/>
        /// </summary>
        /// <param name="presenter"></param>
        /// <param name="processor"></param>
        public AvailabiltyController(SendProcedureAppointmentAvailabilityPresenter presenter, IProcessor processor)
        {
            this.processor = processor;
            this.presenter = presenter;
        }

        /// <summary>
        ///     Send appointment availability of a procedure to subscribers.
        /// </summary>
        /// <remarks>/Assets/sendAppointAvailabilityOfProcedure.md</remarks>
        [HttpPost("procedures")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
        public async Task<IActionResult> SendAppointmentAvailabilityAsync([FromBody] ProcedureAvailability availableProcedures)
        {
            var command = ConvertFrom(availableProcedures);
            await processor.ProcessCommandAsync(command).ConfigureAwait(false);
            return presenter.ViewModel;
        }

        private static SendAppointmentAvailabilityCommand ConvertFrom(ProcedureAvailability sendProcedureAppointment)
        {
            return new SendAppointmentAvailabilityCommand(Resources.PartnerCode,
                sendProcedureAppointment.ProcedureId,
                sendProcedureAppointment.CodeInsee);
        }
    }
}