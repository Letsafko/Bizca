namespace Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;
    public sealed class CreateOrUpdateUserPasswordValidator : AbstractValidator<CreateOrUpdateUserPasswordCommand>
    {
        public CreateOrUpdateUserPasswordValidator()
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