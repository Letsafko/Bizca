namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using System.Collections.Generic;

    public sealed class UserContactToCreateNotificationEvent : INotificationEvent
    {
        public UserContactToCreateNotificationEvent(string partnerCode,
            string email,
            IDictionary<string, object> attributes)
        {
            Attributes = attributes;
            PartnerCode = partnerCode;
            Email = email;
        }

        public IDictionary<string, object> Attributes { get; }
        public string PartnerCode { get; }
        public string Email { get; }
    }
}