namespace Bizca.Bff.Domain.Wrappers.Contact.Requests
{
    using System.Collections.Generic;

    public sealed class UpdateUserContactRequest
    {
        public UpdateUserContactRequest(string email,
            IReadOnlyCollection<string> smtpBlacklistSender = default,
            IReadOnlyCollection<int> unlinkListIds = default,
            IReadOnlyCollection<int> listIds = default,
            IDictionary<string, object> attributes = default,
            bool emailBlacklisted = false,
            bool smsBlacklisted = false)
        {
            SmtpBlacklistSender = smtpBlacklistSender;
            EmailBlacklisted = emailBlacklisted;
            SmsBlacklisted = smsBlacklisted;
            UnlinkListIds = unlinkListIds;
            Attributes = attributes;
            ListIds = listIds;
            Email = email;
        }

        public IReadOnlyCollection<string> SmtpBlacklistSender { get; }
        public IDictionary<string, object> Attributes { get; }
        public IReadOnlyCollection<int> UnlinkListIds { get; }
        public IReadOnlyCollection<int> ListIds { get; }
        public bool EmailBlacklisted { get; }
        public bool SmsBlacklisted { get; }
        public string Email { get; }
    }
}