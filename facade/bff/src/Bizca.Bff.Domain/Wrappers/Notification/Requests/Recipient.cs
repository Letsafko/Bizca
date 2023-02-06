namespace Bizca.Bff.Domain.Wrappers.Notification.Requests
{
    using System.Collections.Generic;

    public class Recipient
    {
        public Recipient(List<int> listIds)
        {
            ListIds = listIds;
        }

        public List<int> ListIds { get; }
    }
}