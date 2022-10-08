namespace Bizca.Bff.Application.UseCases.PaymentSubmitted
{
    using FluentValidation;
    using Properties;

    public sealed class PaymentSubmittedCommandValidator : AbstractValidator<PaymentSubmittedCommand>
    {
        public PaymentSubmittedCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.SubscriptionCode)
                .NotEmpty()
                .WithMessage(Resources.SUBSCRIPTION_CODE_REQUIRED);

            RuleFor(x => x.BundleId)
                .NotEmpty()
                .WithMessage(Resources.BUNDLE_REQUIRED)
                .Must(x => int.TryParse(x, out int bundleId))
                .WithMessage(Resources.BUNDLE_INVALID);
        }
    }
}