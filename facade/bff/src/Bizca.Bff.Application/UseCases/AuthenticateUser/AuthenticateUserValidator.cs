namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using FluentValidation;
    using Properties;

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