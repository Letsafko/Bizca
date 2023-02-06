namespace Bizca.Bff.Application.UseCases.UpdateUser
{
    using Domain.Enumerations;
    using Domain.Properties;
    using FluentValidation;
    using System;

    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED)
                .MaximumLength(20)
                .WithMessage(Resources.EXTERNAL_USERID_MAXIMUM_LENGTH_EXCEED);

            RuleFor(x => x.PhoneNumber)
                .Matches(Resources.PHONE_REGEX)
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage(Resources.PHONE_INVALID);

            RuleFor(x => x.Civility)
                .Must(x => Enum.TryParse<Civility>(x, true, out Civility civility))
                .When(x => !string.IsNullOrWhiteSpace(x.Civility))
                .WithMessage(Resources.CIVILITY_INVALID);

            RuleFor(x => x.Email)
                .Matches(Resources.EMAIL_REGEX)
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage(Resources.EMAIL_INVALID);

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName))
                .WithMessage(Resources.LASTNAME_MAXIMUN_LENGTH_EXCEED);

            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName))
                .WithMessage(Resources.FIRSTNAME_MAXIMUN_LENGTH_EXCEED);
        }
    }
}