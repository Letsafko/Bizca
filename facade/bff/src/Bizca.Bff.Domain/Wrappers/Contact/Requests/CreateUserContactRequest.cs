namespace Bizca.Bff.Domain.Wrappers.Contact.Requests
{
    using System.Collections.Generic;
    public sealed class CreateUserContactRequest
    {
        public CreateUserContactRequest(string email,
            HashSet<int> listIds,
            HashSet<string> smtpBlacklistSender = null,
            bool emailBlacklisted = false,
            bool updatedEnabled = false,
            bool smsBlacklisted = false)
        {
            SmtpBlacklistSender = smtpBlacklistSender;
            EmailBlacklisted = emailBlacklisted;
            UpdatedEnabled = updatedEnabled;
            SmsBlacklisted = smsBlacklisted;
            ListIds = listIds;
            Email = email;
        }

        public IReadOnlyCollection<int> ListIds { get; }
        public IReadOnlyCollection<string> SmtpBlacklistSender { get; }
        public IReadOnlyDictionary<string, object> Attributes => _attributes;
        public bool EmailBlacklisted { get; }
        public bool UpdatedEnabled { get; }
        public bool SmsBlacklisted { get; }
        public string Email { get; }

        private Dictionary<string, object> _attributes;
        public void AddNewAttribute(string key, object value)
        {
            _attributes ??= new Dictionary<string, object>();
            _attributes.TryAdd(key, value);
        }

        public void AddContactAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            foreach (var kv in attributes)
            {
                AddNewAttribute(kv.Key, kv.Value);
            }
        }
    }
}