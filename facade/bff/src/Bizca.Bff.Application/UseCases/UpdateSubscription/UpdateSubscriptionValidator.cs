namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;

    public sealed class UpdateSubscriptionValidator : AbstractValidator<UpdateSubscriptionCommand>
    {
        public UpdateSubscriptionValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.ProcedureTypeId)
                .NotEmpty()
                .WithMessage(Resources.PROCEDURE_TYPE_REQUIRED)
                .Must(x => int.TryParse(x, out int procedureTypeId))
                .WithMessage(Resources.PROCEDURE_TYPE_INVALID);

            RuleFor(x => x.CodeInsee)
                .NotEmpty()
                .WithMessage(Resources.CODE_INSEE_REQUIRED)
                .Must(x => CodeInseeExtensions.IsCodeInseeWellFormatted(x))
                .WithMessage(Resources.CODE_INSEE_MAL_FORMATTED);
        }
    }
}