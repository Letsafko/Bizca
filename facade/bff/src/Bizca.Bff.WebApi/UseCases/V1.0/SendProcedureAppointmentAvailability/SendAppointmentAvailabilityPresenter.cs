namespace Bizca.Bff.WebApi.UseCases.V1._0.SendProcedureAppointmentAvailability
{
    using Bizca.Bff.Application.UseCases.SendAppointmentAvailability;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Send appointment of procedure availability
    /// </summary>
    public class SendProcedureAppointmentAvailabilityPresenter : ISendAppointmentAvailabilityOutput
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