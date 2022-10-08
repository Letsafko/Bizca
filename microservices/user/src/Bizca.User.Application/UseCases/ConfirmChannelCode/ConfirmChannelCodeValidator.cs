namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using FluentValidation;
    using Properties;

    public sealed class ConfirmChannelCodeValidator : AbstractValidator<ChannelConfirmationCommand>
    {
        public ConfirmChannelCodeValidator()
        {
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.CodeConfirmation)
                .NotEmpty()
                .WithMessage(Resources.CONFIRMATION_CODE_REQUIRED);

            RuleFor(x => x.ChannelType)
                .NotNull()
                .WithMessage(Resources.CHANNEL_TYPE_INVALID);
        }
    }
}