﻿namespace Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails
{
    using FluentValidation;
    using Properties;

    public sealed class GetUserSubscriptionDetailsValidator : AbstractValidator<GetUserSubscriptionDetailsQuery>
    {
        public GetUserSubscriptionDetailsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);

            RuleFor(x => x.SubscriptionCode)
                .NotEmpty()
                .WithMessage(Resources.SUBSCRIPTION_CODE_REQUIRED);
        }
    }
}