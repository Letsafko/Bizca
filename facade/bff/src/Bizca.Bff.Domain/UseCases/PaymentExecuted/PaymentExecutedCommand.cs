﻿namespace Bizca.Bff.Application.UseCases.PaymentExecuted
{
    using Core.Domain.Cqrs.Commands;

    public sealed class PaymentExecutedCommand : ICommand
    {
        public PaymentExecutedCommand(string externalUserId,
            string subscriptionCode)
        {
            SubscriptionCode = subscriptionCode;
            ExternalUserId = externalUserId;
        }

        public string SubscriptionCode { get; }
        public string ExternalUserId { get; }
    }
}