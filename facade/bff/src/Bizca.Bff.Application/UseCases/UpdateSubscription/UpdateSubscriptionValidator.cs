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
                .Must(x => int.TryParse(x, out int procedureTypeId))
                .When(x => !string.IsNullOrWhiteSpace(x.ProcedureTypeId))
                .WithMessage(Resources.PROCEDURE_TYPE_INVALID);

            RuleFor(x => x.BundleId)
                .Must(x => int.TryParse(x, out int _))
                .When(x => !string.IsNullOrWhiteSpace(x.BundleId))
                .WithMessage(Resources.BUNDLE_INVALID);
        }
    }
}
