namespace Bizca.Bff.WebApi.UseCases.V10.SendProcedureAppointmentAvailability
{
    /// <summary>
    /// Appointment availability of a procedure
    /// </summary>
    public sealed class SendProcedureAppointmentAvailability
    {
        /// <summary>
        ///  Organism identifier.
        /// </summary>
        public string OrganismId { get; set; }

        /// <summary>
        /// Procedure identifier.
        /// </summary>
        public string ProcedureId { get; set; }

        /// <summary>
        /// Procedure href.
        /// </summary>
        public string ProcedureHref { get; set; }
    }
}
