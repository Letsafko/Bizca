namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class CreateUserContactNotification : IEvent
    {
        public CreateUserContactNotification(string partnerCode, string email)
        {
            PartnerCode = partnerCode;
            Email = email;
        }

        private Dictionary<string, object> _attributes;
        public IReadOnlyDictionary<string, object> Attributes => _attributes;

        public string PartnerCode { get; }
        public string Email { get; }

        internal void AddNewAttribute(string key, object value)
        {
            _attributes ??= new Dictionary<string, object>();
            _attributes.TryAdd(key, value);
        }
    }
}
