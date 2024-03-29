﻿namespace Bizca.Bff.Application.UseCases.GetUserSubscriptions
{
    using Bizca.Bff.Application.Properties;
    using FluentValidation;

    public sealed class GetUserSubscriptionsValidator : AbstractValidator<GetUserSubscriptionsQuery>
    {
        public GetUserSubscriptionsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ExternalUserId)
                .NotEmpty()
                .WithMessage(Resources.EXTERNAL_USERID_REQUIRED);
        }
    }
}