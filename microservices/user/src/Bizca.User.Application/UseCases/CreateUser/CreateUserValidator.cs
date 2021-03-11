namespace Bizca.User.Application.UseCases.CreateUser
{
    using FluentValidation;
    using System;

    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        private const string expression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public CreateUserValidator()
        {
            RuleFor(x => x.PartnerCode).NotEmpty().WithMessage("partnerCode is required.");
            RuleFor(x => x.ExternalUserId).NotEmpty().WithMessage("externalUserId is required.");

            RuleFor(x => x.Civility).NotEmpty().WithMessage("civility is required.");
            RuleFor(x => x.Civility).Must(x => int.TryParse(x, out int civilityId)).WithMessage("civility is invalid.");

            When(x => !string.IsNullOrWhiteSpace(x.EconomicActivity),
                () => RuleFor(x => x.EconomicActivity).Must(x => int.TryParse(x, out int _)).WithMessage("economicActivity is invalid."));

            When(x => !string.IsNullOrWhiteSpace(x.BirthDate),
                () => RuleFor(x => x.BirthDate).Must(x => DateTime.TryParse(x, out DateTime _)).WithMessage("birthDate is invalid."));

            When(x => !string.IsNullOrWhiteSpace(x.BirthCountry),
                () => RuleFor(x => x.BirthCountry).Matches("^[A-Za-z]{2}$").WithMessage("birthCountry is invalid."));

            When(x => !string.IsNullOrWhiteSpace(x.BirthCity),
                () => RuleFor(x => x.BirthCity).MaximumLength(50).WithMessage("birthCity is invalid."));

            When(x => !string.IsNullOrWhiteSpace(x.Email),
                () => RuleFor(x => x.Email).Matches(expression).WithMessage("email is invalid."));

            RuleFor(x => x.LastName).NotEmpty().WithMessage("lastName is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("firstName is required.");

            When(x => !string.IsNullOrWhiteSpace(x.Address?.Name), () => RuleFor(x => x.Address.Name).MaximumLength(100).WithMessage("addressName is invalid."));
            When(x => !string.IsNullOrWhiteSpace(x.Address?.ZipCode), () => RuleFor(x => x.Address.ZipCode).MaximumLength(5).WithMessage("zipCode is invalid."));
            When(x => !string.IsNullOrWhiteSpace(x.Address?.Street), () => RuleFor(x => x.Address.Street).MaximumLength(255).WithMessage("street is invalid."));

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.Country).NotEmpty().WithMessage("country is required.");
                RuleFor(x => x.Address.Country).Matches("^[A-Za-z]{2}$").WithMessage("country is invalid.");

                RuleFor(x => x.Address.City).NotEmpty().WithMessage("city is required.");
                RuleFor(x => x.Address.City).MaximumLength(100).WithMessage("city is invalid.");
            });
        }
    }
}