namespace Bizca.Bff.WebApi.UseCases.V10.SendProcedureAppointmentAvailability
{
    /// <summary>
    /// Appointment availability of a procedure
    /// </summary>
    public sealed class SendProcedureAppointmentAvailability
    {
        /// <summary>
        ///  Organism code identifier.
        /// </summary>
        public string CodeInsee { get; set; }

        /// <summary>
        /// Procedure identifier.
        /// </summary>
        public string ProcedureId { get; set; }
    }
}
