﻿namespace Bizca.Bff.Application.UseCases.SendProcedureAppointmentAvailability
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;

    public sealed class SendProcedureAppointmentAvailabilityCommandValidator : AbstractValidator<SendProcedureAppointmentAvailabilityCommand>
    {
        public SendProcedureAppointmentAvailabilityCommandValidator()
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
