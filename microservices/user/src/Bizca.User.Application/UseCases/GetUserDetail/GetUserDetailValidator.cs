namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using FluentValidation;
    public sealed class GetUserDetailValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailValidator()
        {
            RuleFor(x => x.PartnerCode).NotNull().WithMessage("partnerCode is required.");
            RuleFor(x => x.ExternalUserId).NotNull().WithMessage("externalUserId is required.");
        }
    }
}