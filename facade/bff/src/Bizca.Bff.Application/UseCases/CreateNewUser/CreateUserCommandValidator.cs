namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Bff.Application.Properties;
    using Bizca.Bff.Domain.Enumerations;
    using FluentValidation;
    using System;

    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.Civility)
                .NotEmpty()
                .WithMessage(Resources.CIVILITY_REQUIRED)
                .Must(x => Enum.TryParse<Civility>(x, true, out var civility))
                .WithMessage(Resources.CIVILITY_INVALID);

            RuleFor(x => x.EconomicActivity)
                .Must(x => int.TryParse(x, out int _))
                .When(x => !string.IsNullOrWhiteSpace(x.EconomicActivity))
                .WithMessage(Resources.ECONOMIC_ACTIVITY_INVALID);

            RuleFor(x => x.Email)
                .Matches(Resources.EMAIL_REGEX)
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage(Resources.EMAIL_INVALID);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(Resources.LASTNAME_REQUIRED);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(Resources.FIRSTNAME_REQUIRED);
        }
    }
}