namespace Bizca.Bff.Domain.Events
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class UserContactToCreateEvent : IEvent
    {
        public UserContactToCreateEvent(string partnerCode,
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
