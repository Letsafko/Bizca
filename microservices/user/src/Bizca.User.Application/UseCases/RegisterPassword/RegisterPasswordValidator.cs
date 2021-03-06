namespace Bizca.User.Application.UseCases.RegisterPassword
{
    using FluentValidation;

    public sealed class RegisterPasswordCommandValidator : AbstractValidator<RegisterPasswordCommand>
    {
        public RegisterPasswordCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("password is required.");
            RuleFor(x => x.PartnerCode).NotEmpty().WithMessage("partnerCode is required.");
            RuleFor(x => x.ChannelResource).NotEmpty().WithMessage("externalUserId is required.");
        }
    }
}