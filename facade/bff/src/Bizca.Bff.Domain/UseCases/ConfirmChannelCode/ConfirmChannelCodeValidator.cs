namespace Bizca.Bff.Application.UseCases.ConfirmChannelCode
{
    using Domain.Enumerations;
    using Domain.Properties;
    using FluentValidation;
    using System;

    public sealed class ConfirmChannelCodeValidator : AbstractValidator<ConfirmChannelCodeCommand>
    {
        public ConfirmChannelCodeValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.ConfirmationCode)
                .NotEmpty()
                .WithMessage(Resources.CONFIRMATION_CODE_REQUIRED);

            RuleFor(x => x.ChannelType)
                .NotNull()
                .WithMessage(Resources.CHANNEL_TYPE_REQUIRED)
                .Must(x => Enum.TryParse(typeof(ChannelType), x, true, out object _))
                .WithMessage(Resources.CHANNEL_TYPE_INVALID);
        }
    }
}