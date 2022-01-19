namespace Bizca.Bff.Application.UseCases.SendProcedureAppointmentAvailability
{
    using Bizca.Core.Application.Commands;
    public sealed class SendProcedureAppointmentAvailabilityCommand : ICommand
    {
        public SendProcedureAppointmentAvailabilityCommand(string partnerCode,
            string procedureId,
            string codeInsee)
        {
            PartnerCode = partnerCode;
            ProcedureId = procedureId;
            CodeInsee = codeInsee;
        }

        public string PartnerCode { get; }
        public string ProcedureId { get; }
        public string CodeInsee { get; }
    }
}
