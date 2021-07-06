namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;
    public sealed class AuthenticateUserValidator : AbstractValidator<AuthenticateUserQuery>
    {
        public AuthenticateUserValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Resources.PASSWORD_REQUIRED);

            RuleFor(x => x.Resource)
                .NotEmpty()
                .WithMessage(Resources.RESOURCE_LOGIN_REQUIRED);
        }
    }
}
