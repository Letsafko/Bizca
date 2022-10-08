namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    using FluentValidation;
    using Properties;

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