namespace Bizca.User.Application.UseCases.GetUsersByCriteria
{
    using FluentValidation;
    using System;
    public sealed class GetUsersValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersValidator()
        {
            RuleFor(x => x.PartnerCode).NotNull().WithMessage("partnerCode is required.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("page size should be greater than zero.");
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("page number should be greater than zero.");
            RuleFor(x => x.BirthDate).Must(x => string.IsNullOrWhiteSpace(x) || DateTime.TryParse(x, out DateTime birthday)).WithMessage("birthdate is invalid.");
            RuleFor(x => x.Direction).NotNull().WithMessage("direction is required.")
                                     .Must(x => x.ToLower().Equals(SearchDirection.Next) || x.ToLower().Equals(SearchDirection.Previous))
                                     .WithMessage("direction should be next or previous.");
        }
    }
}