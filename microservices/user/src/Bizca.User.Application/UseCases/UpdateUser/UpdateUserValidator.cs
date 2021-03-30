namespace Bizca.User.Application.UseCases.UpdateUser
{
    using FluentValidation;
    using System;
    using System.Text.RegularExpressions;

    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        private const string expression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public UpdateUserValidator()
        {
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage("partnerCode is required.");

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage("externalUserId is required.");

            RuleFor(x => x.Civility)
                .Must(x => int.TryParse(x, out int civilityId))
                .When(x => !string.IsNullOrWhiteSpace(x.Civility))
                .WithMessage("civility is invalid.");

            RuleFor(x => x.EconomicActivity)
                .Must(x => int.TryParse(x, out int activityId))
                .When(x => !string.IsNullOrWhiteSpace(x.EconomicActivity))
                .WithMessage("economicActivity is invalid.");

            RuleFor(x => x.BirthDate)
                .Must(x => DateTime.TryParse(x, out DateTime bithday))
                .When(x => !string.IsNullOrWhiteSpace(x.BirthDate))
                .WithMessage("birthDate is invalid.");

            RuleFor(x => x.Email)
                .Must(x => Regex.IsMatch(x, expression))
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("email is invalid.");
        }
    }
}