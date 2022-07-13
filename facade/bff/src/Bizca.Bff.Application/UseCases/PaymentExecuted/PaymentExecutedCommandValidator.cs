namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;

    public sealed class PaymentExecutedCommandValidator : AbstractValidator<PaymentExecutedCommand>
    {
        public PaymentExecutedCommandValidator()
        {
            RuleFor(x => x.SubscriptionCode)
                .NotNull()
                .WithMessage(Resources.SUBSCRIPTION_CODE_REQUIRED);
        }
    }
}
