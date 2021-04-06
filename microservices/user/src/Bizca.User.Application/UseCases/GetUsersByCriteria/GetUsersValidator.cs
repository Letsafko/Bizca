namespace Bizca.User.Application.UseCases.GetUsersByCriteria
{
    using Bizca.User.Application.Properties;
    using FluentValidation;
    using System;
    public sealed class GetUsersValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PartnerCode)
                .NotNull()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage(Resources.SEARCH_PAGE_SIZE_GREATER_THAN_ZERO);

            RuleFor(x => x.BirthDate)
                .Must(x => string.IsNullOrWhiteSpace(x) || DateTime.TryParse(x, out DateTime birthday))
                .WithMessage(Resources.BIRTHDATE_INVALID);

            RuleFor(x => x.Direction)
                .NotNull()
                .WithMessage(Resources.SEARCH_DIRECTION_REQUIRED)
                .Must(x => x.ToLower().Equals(SearchDirection.Next) || x.ToLower().Equals(SearchDirection.Previous))
                .WithMessage(Resources.SEARCH_DIRECTION_BAD_VALUE);
        }
    }
}