namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;

    public sealed class CreateSubscriptionValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CodeInsee)
                .NotEmpty()
                .WithMessage(Resources.CODE_INSEE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.ProcedureTypeId)
                .NotEmpty()
                .WithMessage(Resources.PROCEDURE_TYPE_REQUIRED)
                .Must(x => int.TryParse(x, out int procedureTypeId))
                .WithMessage(Resources.PROCEDURE_TYPE_INVALID);

            RuleFor(x => x.BundleId)
                .Must(x => int.TryParse(x, out int _))
                .When(x => !string.IsNullOrWhiteSpace(x.BundleId))
                .WithMessage(Resources.BUNDLE_INVALID);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage(Resources.PHONENUMBER_REQUIRED);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(Resources.LASTNAME_REQUIRED);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(Resources.FIRSTNAME_REQUIRED);

            RuleFor(x => x.Email)
                .Matches(Resources.EMAIL_REGEX)
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage(Resources.EMAIL_INVALID);
        }
    }
}
