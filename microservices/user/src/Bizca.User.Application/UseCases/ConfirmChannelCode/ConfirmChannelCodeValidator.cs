namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using FluentValidation;

    public sealed class ConfirmChannelCodeValidator : AbstractValidator<ChannelConfirmationCommand>
    {
        public ConfirmChannelCodeValidator()
        {
            RuleFor(x => x.PartnerCode).NotEmpty().WithMessage("partnerCode is required.");
            RuleFor(x => x.ExternalUserId).NotEmpty().WithMessage("externalUserId is required.");
            RuleFor(x => x.CodeConfirmation).NotEmpty().WithMessage("confirmationCode is required.");
            RuleFor(x => x.ChannelType).NotNull().WithMessage("channelType is invalid.");
        }
    }
}