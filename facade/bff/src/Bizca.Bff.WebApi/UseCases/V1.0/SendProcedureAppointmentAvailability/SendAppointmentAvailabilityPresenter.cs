﻿using Bizca.Bff.Application.UseCases.SendProcedureAppointmentAvailability;
using Microsoft.AspNetCore.Mvc;

namespace Bizca.Bff.WebApi.UseCases.V10.SendProcedureAppointmentAvailability
{
    /// <summary>
    ///    Send appointment of procedure availability
    /// </summary>
    public class SendProcedureAppointmentAvailabilityPresenter : ISendProcedureAppointmentAvailabilityOutput
    {
        /// <summary>
        ///     Get procedures for active subscriptions view model.
        /// </summary>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="result"></param>
        public void Ok(bool result)
        {
            ViewModel = new OkObjectResult(new { Success = result });
        }
    }
}