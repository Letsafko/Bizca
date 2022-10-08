namespace Bizca.Bff.Application.UseCases.SendAppointmentAvailability
{
    using Core.Application.Commands;

    public sealed class SendAppointmentAvailabilityCommand : ICommand
    {
        public SendAppointmentAvailabilityCommand(string partnerCode,
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