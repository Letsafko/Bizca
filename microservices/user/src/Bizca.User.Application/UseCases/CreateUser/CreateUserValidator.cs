namespace Bizca.User.Application.UseCases.CreateUser
{
    using FluentValidation;
    using System;
    using System.Text.RegularExpressions;

    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        private const string expression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public CreateUserValidator()
        {
            RuleFor(x => x.PartnerCode).NotEmpty().WithMessage("partnerCode is required.");
            RuleFor(x => x.ExternalUserId).NotEmpty().WithMessage("externalUserId is required.");

            RuleFor(x => x.Civility).NotEmpty().WithMessage("civility is required.");
            RuleFor(x => x.Civility).Must(x => int.TryParse(x, out int civilityId)).WithMessage("civility is invalid.");

            RuleFor(x => x.EconomicActivity).NotEmpty().WithMessage("economicActivity is required.");
            RuleFor(x => x.EconomicActivity).Must(x => int.TryParse(x, out int activityId)).WithMessage("economicActivity is invalid.");

            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("birthDate is required.");
            RuleFor(x => x.BirthDate).Must(x => DateTime.TryParse(x, out DateTime bithday)).WithMessage("birthDate is invalid.");

            RuleFor(x => x.BirthCity).NotEmpty().WithMessage("birthCity is required.");
            RuleFor(x => x.BirthCountry).NotEmpty().WithMessage("birthCountry is required.");

            RuleFor(x => x.Email).Must(x => Regex.IsMatch(x, expression))
                                 .When(x => !string.IsNullOrWhiteSpace(x.Email))
                                 .WithMessage("email is invalid.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("lastName is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("firstName is required.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("phoneNumber is required.");
            RuleFor(x => x.Whatsapp).NotEmpty().WithMessage("whatsappNumber is required.");

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.City).NotEmpty().WithMessage("city is required.");
                RuleFor(x => x.Address.Country).NotEmpty().WithMessage("country is required.");
            });
        }
    }
}