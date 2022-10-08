namespace Bizca.Bff.Application.UseCases.FreezeSubscription
{
    using FluentValidation;
    using Properties;

    public sealed class FreezeSubscriptionCommandValidator : AbstractValidator<FreezeSubscriptionCommand>
    {
        public FreezeSubscriptionCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.Freeze)
                .NotEmpty()
                .WithMessage(Resources.VALUE_CANNOT_BE_NULL_OR_EMPTY)
                .Must(x => bool.TryParse(x, out bool isFreeze))
                .WithMessage(Resources.INVALID_DATA_INPUT);
        }
    }
}