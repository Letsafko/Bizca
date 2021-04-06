namespace Bizca.User.Application.UseCases.CreateUser
{
    using Bizca.User.Application.Properties;
    using FluentValidation;
    using System;

    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            CascadeMode =  CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PartnerCode)
                .NotEmpty()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.Civility)
                .NotEmpty()
                .WithMessage(Resources.CIVILITY_REQUIRED)
                .Must(x => int.TryParse(x, out int civilityId))
                .WithMessage(Resources.CIVILITY_INVALID);

            RuleFor(x => x.EconomicActivity)
                .Must(x => int.TryParse(x, out int _))
                .When(x => !string.IsNullOrWhiteSpace(x.EconomicActivity))
                .WithMessage(Resources.ECONOMIC_ACTIVITY_REQUIRED);

            RuleFor(x => x.BirthDate)
                .Must(x => DateTime.TryParse(x, out DateTime _))
                .When(x => !string.IsNullOrWhiteSpace(x.BirthDate))
                .WithMessage(Resources.BIRTHDATE_INVALID);

            RuleFor(x => x.BirthCountry)
                .Matches(Resources.COUNTRY_CODE_REGEX)
                .When(x => !string.IsNullOrWhiteSpace(x.BirthCountry))
                .WithMessage(Resources.BIRTHCOUNTRY_INVALID);

            RuleFor(x => x.BirthCity)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.BirthCity))
                .WithMessage(Resources.BIRTHCITY_INVALID);

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

            RuleFor(x => x.Address.Name)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.Address?.Name))
                .WithMessage(Resources.ADDRESSNAME_INVALID);

            RuleFor(x => x.Address.ZipCode)
                .MaximumLength(5)
                .When(x => !string.IsNullOrWhiteSpace(x.Address?.ZipCode))
                .WithMessage(Resources.ADDRESS_ZIPCODE_INVALID);

            RuleFor(x => x.Address.Street)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.Address?.Street))
                .WithMessage(Resources.ADDRESS_STREET_INVALID);

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.Country)
                    .NotEmpty()
                    .WithMessage(Resources.COUNTRY_CODE_REQUIRED)
                    .Matches(Resources.COUNTRY_CODE_REGEX)
                    .WithMessage(Resources.COUNTRY_CODE_INVALID);

                RuleFor(x => x.Address.City)
                    .NotEmpty()
                    .WithMessage(Resources.CITY_REQUIRED)
                    .MaximumLength(100)
                    .WithMessage(Resources.CITY_INVALID);
            });
        }
    }
}