namespace Bizca.User.Application.UseCases.UpdateUser
{
    using FluentValidation;
    using Properties;
    using System;

    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.Civility)
                .Must(x => int.TryParse(x, out int civilityId))
                .When(x => !string.IsNullOrWhiteSpace(x.Civility))
                .WithMessage(Resources.CIVILITY_INVALID);

            RuleFor(x => x.EconomicActivity)
                .Must(x => int.TryParse(x, out int activityId))
                .When(x => !string.IsNullOrWhiteSpace(x.EconomicActivity))
                .WithMessage(Resources.ECONOMIC_ACTIVITY_INVALID);

            RuleFor(x => x.BirthDate)
                .Must(x => DateTime.TryParse(x, out DateTime bithday))
                .When(x => !string.IsNullOrWhiteSpace(x.BirthDate))
                .WithMessage(Resources.BIRTHDATE_INVALID);

            RuleFor(x => x.Email)
                .Matches(Resources.EMAIL_REGEX)
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage(Resources.EMAIL_INVALID);
        }
    }
}