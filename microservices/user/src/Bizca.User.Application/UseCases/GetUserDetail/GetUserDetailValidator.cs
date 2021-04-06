namespace Bizca.User.Application.UseCases.GetUserDetail
{
    using Bizca.User.Application.Properties;
    using FluentValidation;
    public sealed class GetUserDetailValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailValidator()
        {
            RuleFor(x => x.PartnerCode)
                .NotNull()
                .WithMessage(Resources.PARTNER_CODE_REQUIRED);

            RuleFor(x => x.ExternalUserId)
                .NotNull()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);
        }
    }
}