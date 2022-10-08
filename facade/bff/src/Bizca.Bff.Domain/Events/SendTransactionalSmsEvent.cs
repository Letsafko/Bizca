﻿namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Wrappers;

    public sealed class SendTransactionalSmsEvent : IEvent
    {
        public SendTransactionalSmsEvent(string sender,
            string recipientPhoneNumber,
            string content)
        {
            Recipient = recipientPhoneNumber;
            Content = content;
            Sender = sender;
        }

        public string TransactionType => Constants.TransactionType.Transactional;
        public string Recipient { get; }
        public string Content { get; }
        public string Sender { get; }
    }
}