namespace Bizca.User.Application.UseCases.RegisterPassword
{
    using FluentValidation;
    using Properties;

    public sealed class RegisterPasswordCommandValidator : AbstractValidator<RegisterPasswordCommand>
    {
        public RegisterPasswordCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Resources.PASSWORD_REQUIRED);

            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ChannelResource)
                .NotEmpty()
                .WithMessage(Resources.CHANNEL_RESOURCE_REQUIRED);
        }
    }
}