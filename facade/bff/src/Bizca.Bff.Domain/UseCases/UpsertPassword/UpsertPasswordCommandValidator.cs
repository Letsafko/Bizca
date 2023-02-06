namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    using Domain.Properties;
    using FluentValidation;

    public sealed class UpsertPasswordCommandValidator : AbstractValidator<UpsertPasswordCommand>
    {
        public UpsertPasswordCommandValidator()
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