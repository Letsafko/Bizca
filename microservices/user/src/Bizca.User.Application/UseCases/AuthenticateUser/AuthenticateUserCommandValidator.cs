namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    using Bizca.User.Application.Properties;
    using FluentValidation;

    public sealed class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Resources.PASSWORD_REQUIRED);

            RuleFor(x => x.ResourceLogin)
                .NotEmpty()
                .WithMessage(Resources.RESOURCE_LOGIN_REQUIRED);

            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);
        }
    }
}