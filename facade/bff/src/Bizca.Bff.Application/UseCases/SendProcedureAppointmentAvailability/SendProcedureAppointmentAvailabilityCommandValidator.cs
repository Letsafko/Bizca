namespace Bizca.Bff.Application.UseCases.SendProcedureAppointmentAvailability
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;

    public sealed class SendProcedureAppointmentAvailabilityCommandValidator : AbstractValidator<SendProcedureAppointmentAvailabilityCommand>
    {
        public SendProcedureAppointmentAvailabilityCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.OrganismId)
                .NotEmpty()
                .WithMessage(Resources.ORGANISM_IDENTIFIER_REQUIRED)
                .Must(x => int.TryParse(x, out int _))
                .WithMessage(Resources.ORGANISM_IDENTIFIER_INVALID);

            RuleFor(x => x.ProcedureId)
                .NotEmpty()
                .WithMessage(Resources.PROCEDURE_TYPE_REQUIRED)
                .Must(x => int.TryParse(x, out int _))
                .WithMessage(Resources.PROCEDURE_TYPE_INVALID);
        }
    }
}
