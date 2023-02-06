﻿namespace Bizca.Bff.WebApi.UseCases.V1._0.SendProcedureAppointmentAvailability
{
    /// <summary>
    ///     Appointment availability of a procedure
    /// </summary>
    public sealed class ProcedureAvailability
    {
        /// <summary>
        ///     Organism code identifier.
        /// </summary>
        public string CodeInsee { get; set; }

        /// <summary>
        ///     Procedure identifier.
        /// </summary>
        public string ProcedureId { get; set; }
    }
}