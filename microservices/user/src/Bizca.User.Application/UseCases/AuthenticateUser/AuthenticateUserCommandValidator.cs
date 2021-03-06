namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    using FluentValidation;

    public sealed class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(x => x.ResourceLogin).NotEmpty().WithMessage("resourceLogin is required.");
            RuleFor(x => x.PartnerCode).NotEmpty().WithMessage("partnerCode is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("password is required.");
        }
    }
}