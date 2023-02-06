namespace Bizca.Bff.Domain.Wrappers.Contact.Requests
{
    using System.Collections.Generic;

    public sealed class CreateUserContactRequest
    {
        public CreateUserContactRequest(string email,
            ICollection<int> listIds,
            ICollection<string> smtpBlacklistSender = null,
            IDictionary<string, object> attributes = default,
            bool emailBlacklisted = false,
            bool updatedEnabled = false,
            bool smsBlacklisted = false)
        {
            SmtpBlacklistSender = smtpBlacklistSender;
            EmailBlacklisted = emailBlacklisted;
            UpdatedEnabled = updatedEnabled;
            SmsBlacklisted = smsBlacklisted;
            Attributes = attributes;
            ListIds = listIds;
            Email = email;
        }

        public ICollection<string> SmtpBlacklistSender { get; }
        public IDictionary<string, object> Attributes { get; }
        public ICollection<int> ListIds { get; }
        public bool EmailBlacklisted { get; }
        public bool UpdatedEnabled { get; }
        public bool SmsBlacklisted { get; }
        public string Email { get; }
    }
}