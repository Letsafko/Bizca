namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    using FluentValidation;
    using Properties;

    public sealed class ReInitializedPasswordCommandValidator : AbstractValidator<ReInitializedPasswordCommand>
    {
        public ReInitializedPasswordCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Resources.EMAIL_REQUIRED)
                .Matches(Resources.EMAIL_REGEX)
                .WithMessage(Resources.EMAIL_INVALID);
        }
    }
}