namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation;

using FluentValidation;
using Properties;

public sealed class RegisterCodeConfirmationValidator : AbstractValidator<RegisterCodeConfirmationCommand>
{
    public RegisterCodeConfirmationValidator()
    {
        RuleFor(x => x.PartnerCode)
            .NotEmpty()
            .WithMessage(Resources.PARTNER_CODE_REQUIRED);

        RuleFor(x => x.ExternalUserId)
            .NotEmpty()
            .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

        RuleFor(x => x.ChannelType)
            .NotNull()
            .WithMessage(Resources.CHANNEL_TYPE_INVALID);
    }
}