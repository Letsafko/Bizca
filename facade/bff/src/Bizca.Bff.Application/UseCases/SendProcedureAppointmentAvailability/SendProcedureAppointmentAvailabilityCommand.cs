namespace Bizca.Bff.Application.UseCases.SendProcedureAppointmentAvailability
{
    using Bizca.Core.Application.Commands;
    public sealed class SendProcedureAppointmentAvailabilityCommand : ICommand
    {
        public SendProcedureAppointmentAvailabilityCommand(string partnerCode,
            string procedureHref,
            string procedureId,
            string organismId)
        {
            ProcedureHref = procedureHref;
            PartnerCode = partnerCode;
            ProcedureId = procedureId;
            OrganismId = organismId;
        }

        public string ProcedureHref { get; }
        public string PartnerCode { get; }
        public string ProcedureId { get; }
        public string OrganismId { get; }
    }
}
