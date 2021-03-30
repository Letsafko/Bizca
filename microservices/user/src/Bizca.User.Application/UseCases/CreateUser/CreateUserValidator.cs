namespace Bizca.User.Application.UseCases.CreateUser
{
    using FluentValidation;
    using System;

    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        private const string expression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public CreateUserValidator()
        {
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage("partnerCode is required.");

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage("externalUserId is required.");

            RuleFor(x => x.Civility)
                .NotEmpty()
                .WithMessage("civility is required.")
                .Must(x => int.TryParse(x, out int civilityId))
                .WithMessage("civility is invalid.");

            RuleFor(x => x.EconomicActivity)
                .Must(x => int.TryParse(x, out int _))
                .When(x => !string.IsNullOrWhiteSpace(x.EconomicActivity))
                .WithMessage("economicActivity is invalid.");

            RuleFor(x => x.BirthDate)
                .Must(x => DateTime.TryParse(x, out DateTime _))
                .When(x => !string.IsNullOrWhiteSpace(x.BirthDate))
                .WithMessage("birthDate is invalid.");

            RuleFor(x => x.BirthCountry)
                .Matches("^[A-Za-z]{2}$")
                .When(x => !string.IsNullOrWhiteSpace(x.BirthCountry))
                .WithMessage("birthCountry is invalid.");

            RuleFor(x => x.BirthCity)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.BirthCity))
                .WithMessage("birthCity is invalid.");

            RuleFor(x => x.Email)
                .Matches(expression)
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("email is invalid.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("lastName is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("firstName is required.");

            RuleFor(x => x.Address.Name)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.Address?.Name))
                .WithMessage("addressName is invalid.");

            RuleFor(x => x.Address.ZipCode)
                .MaximumLength(5)
                .When(x => !string.IsNullOrWhiteSpace(x.Address?.ZipCode))
                .WithMessage("zipCode is invalid.");

            RuleFor(x => x.Address.Street)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.Address?.Street))
                .WithMessage("street is invalid.");

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.Country)
                    .NotEmpty()
                    .WithMessage("country is required.");

                RuleFor(x => x.Address.Country)
                    .Matches("^[A-Za-z]{2}$")
                    .WithMessage("country is invalid.");

                RuleFor(x => x.Address.City)
                    .NotEmpty()
                    .WithMessage("city is required.");

                RuleFor(x => x.Address.City)
                    .MaximumLength(100)
                    .WithMessage("city is invalid.");
            });
        }
    }
}