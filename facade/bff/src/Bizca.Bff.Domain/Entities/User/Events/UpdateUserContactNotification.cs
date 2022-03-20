namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class UpdateUserContactNotification : IEvent
    {
        public UpdateUserContactNotification(string email,
            HashSet<int> unlinkListIds = default,
            HashSet<int> listIds = default,
            bool emailBlacklisted = false,
            bool smsBlacklisted = false)
        {
            EmailBlacklisted = emailBlacklisted;
            SmsBlacklisted = smsBlacklisted;
            UnlinkListIds = unlinkListIds;
            ListIds = listIds;
            Email = email;

        }

        public IReadOnlyCollection<string> SmtpBlacklistSender => _smtpBlacklistSender;
        public IReadOnlyDictionary<string, object> Attributes => _attributes;
        public IReadOnlyCollection<int> UnlinkListIds { get; }
        public IReadOnlyCollection<int> ListIds { get; }
        public bool EmailBlacklisted { get; }
        public bool SmsBlacklisted { get; }
        public string Email { get; }

        private Dictionary<string, object> _attributes;
        private HashSet<string> _smtpBlacklistSender;
        public void AddNewAttribute(string key, object value)
        {
            _attributes ??= new Dictionary<string, object>();
            _attributes.TryAdd(key, value);
        }

        public void AddContactAttributes(IDictionary<string, object> attributes)
        {
            foreach (var kv in attributes)
            {
                AddNewAttribute(kv.Key, kv.Value);
            }
        }

        public void AddSenderBlackList(string sender)
        {
            _smtpBlacklistSender ??= new HashSet<string>();
            _smtpBlacklistSender.Add(sender);
        }
    }
}
