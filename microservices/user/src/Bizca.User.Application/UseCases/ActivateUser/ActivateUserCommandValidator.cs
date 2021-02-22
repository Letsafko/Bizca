namespace Bizca.User.Application.UseCases.ActivateUser
{
    using FluentValidation;

    public sealed class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator()
        {
            RuleFor(x => x.Activate).NotEmpty().WithMessage("activate field is required.");
            RuleFor(x => x.Activate).Must(IsBoolean).WithMessage("activate field is invalid.");

            RuleFor(x => x.PartnerCode).NotEmpty().WithMessage("partnerCode is required.");
            RuleFor(x => x.ExternalUserId).NotEmpty().WithMessage("externalUserId is required.");
        }

        private bool IsBoolean(string value)
        {
            return bool.TryParse(value, out bool _);
        }
    }
}
