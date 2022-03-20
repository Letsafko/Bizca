namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class CreateUserContactNotification : IEvent
    {
        public CreateUserContactNotification(string email,
            bool emailBlacklisted = false,
            bool smsBlacklisted = false,
            bool updateEnabled = false)
        {
            EmailBlacklisted = emailBlacklisted;
            SmsBlacklisted = smsBlacklisted;
            UpdateEnabled = updateEnabled;
            Email = email;
        }

        private Dictionary<string, object> _attributes;
        public IReadOnlyDictionary<string, object> Attributes => _attributes;

        private HashSet<int> _listIds;
        public IReadOnlyList<int> ListIds => _listIds?.ToList();

        public bool EmailBlacklisted { get; }
        public bool SmsBlacklisted { get; }
        public bool UpdateEnabled { get; }
        public string Email { get; }

        internal void AddNewAttribute(string key, object value)
        {
            _attributes ??= new Dictionary<string, object>();
            _attributes.TryAdd(key, value);
        }

        internal void AddNewContactList(int listId)
        {
            _listIds ??= new HashSet<int>();
            _listIds.Add(listId);
        }

        internal void AddContactListIds(ICollection<int> listIds)
        {
            foreach (var listId in listIds)
            {
                AddNewContactList(listId);
            }
        }
    }
}
