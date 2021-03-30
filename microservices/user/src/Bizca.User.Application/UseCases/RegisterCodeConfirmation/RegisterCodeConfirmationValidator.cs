namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using FluentValidation;

    public sealed class RegisterCodeConfirmationValidator : AbstractValidator<RegisterCodeConfirmationCommand>
    {
        public RegisterCodeConfirmationValidator()
        {
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage("partnerCode is required.");

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage("externalUserId is required.");

            RuleFor(x => x.ChannelType)
                .NotNull()
                .WithMessage("channelType is invalid.");
        }
    }
}