namespace Bizca.Bff.Application.UseCases.SendAppointmentAvailability
{
    using FluentValidation;
    using Properties;

    public sealed class
        SendAppointmentAvailabilityCommandValidator : AbstractValidator<SendAppointmentAvailabilityCommand>
    {
        public SendAppointmentAvailabilityCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CodeInsee)
                .NotEmpty()
                .WithMessage(Resources.CODE_INSEE_REQUIRED);

            RuleFor(x => x.ProcedureId)
                .NotEmpty()
                .WithMessage(Resources.PROCEDURE_TYPE_REQUIRED)
                .Must(x => int.TryParse(x, out int _))
                .WithMessage(Resources.PROCEDURE_TYPE_INVALID);
        }
    }
}