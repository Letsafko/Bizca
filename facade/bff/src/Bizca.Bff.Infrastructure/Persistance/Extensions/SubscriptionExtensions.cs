namespace Bizca.Bff.Infrastructure.Persistance.Extensions
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class SubscriptionExtensions
    {
        public static DataTable ToDataTable(this IEnumerable<Subscription> subscriptions, string typeName)
        {
            var dt = new DataTable(typeName);
            dt.Columns.Add(SubscriptionColumns.SubscriptionId, typeof(int));
            dt.Columns.Add(SubscriptionColumns.SubscriptionCode, typeof(string));
            dt.Columns.Add(SubscriptionColumns.SubscriptionStatusId, typeof(int));
            dt.Columns.Add(SubscriptionColumns.ProcedureTypeId, typeof(int));
            dt.Columns.Add(SubscriptionColumns.OrganismId, typeof(int));
            dt.Columns.Add(SubscriptionColumns.BundleId, typeof(int));
            dt.Columns.Add(SubscriptionColumns.Freeze, typeof(bool));
            dt.Columns.Add(SubscriptionColumns.Amount, typeof(decimal));

            dt.Columns.Add(SubscriptionColumns.FirstName, typeof(string));
            dt.Columns.Add(SubscriptionColumns.LastName, typeof(string));
            dt.Columns.Add(SubscriptionColumns.PhoneNumber, typeof(string));
            dt.Columns.Add(SubscriptionColumns.Whatsapp, typeof(string));
            dt.Columns.Add(SubscriptionColumns.Email, typeof(string));

            dt.Columns.Add(SubscriptionColumns.WhatsappCounter, typeof(int));
            dt.Columns.Add(SubscriptionColumns.TotalWhatsapp, typeof(int));
            dt.Columns.Add(SubscriptionColumns.EmailCounter, typeof(int));
            dt.Columns.Add(SubscriptionColumns.TotalEmail, typeof(int));
            dt.Columns.Add(SubscriptionColumns.SmsCounter, typeof(int));
            dt.Columns.Add(SubscriptionColumns.TotalSms, typeof(int));

            dt.Columns.Add(SubscriptionColumns.ActivatedChannelMask, typeof(int));
            dt.Columns.Add(SubscriptionColumns.ConfirmedChannelMask, typeof(int));

            dt.Columns.Add(SubscriptionColumns.BeginDate, typeof(DateTime));
            dt.Columns.Add(SubscriptionColumns.EndDate, typeof(DateTime));
            subscriptions
                ?.ToList()
                .ForEach(x =>
                {
                    dt.Rows.Add
                    (
                        x.Id,
                        x.SubscriptionCode.ToString(),
                        (int)x.SubscriptionState.Status,
                        x.Procedure.ProcedureType.Id,
                        x.Procedure.Organism.Id,
                        x.Bundle?.BundleIdentifier?.Id,
                        x.SubscriptionSettings?.IsFreeze,
                        x.Price?.Amount,

                        x.UserSubscription.FirstName,
                        x.UserSubscription.LastName,
                        x.UserSubscription.PhoneNumber,
                        x.UserSubscription.Whatsapp,
                        x.UserSubscription.Email,

                        x.SubscriptionSettings?.WhatsappCounter,
                        x.SubscriptionSettings?.TotalWhatsapp,
                        x.SubscriptionSettings?.EmailCounter,
                        x.SubscriptionSettings?.TotalEmail,
                        x.SubscriptionSettings?.SmsCounter,
                        x.SubscriptionSettings?.TotalSms,

                        (int)x.UserSubscription.ChannelActivationStatus,
                        (int)x.UserSubscription.ChannelConfirmationStatus,

                        x.SubscriptionSettings?.BeginDate,
                        x.SubscriptionSettings?.EndDate
                    );
                });

            return dt;
        }

        public static DataTable ToDataTable(this IEnumerable<SubscriberAvailability> subscribers, string typeName)
        {
            var dt = new DataTable(typeName);
            dt.Columns.Add(SubscriptionColumns.SubscriptionId, typeof(int));
            dt.Columns.Add(SubscriptionColumns.EmailCounter, typeof(int));
            dt.Columns.Add(SubscriptionColumns.SmsCounter, typeof(int));
            foreach (var subscriber in subscribers)
            {
                dt.Rows.Add
                (
                    subscriber.SubscriptionId,
                    subscriber.EmailCounter,
                    subscriber.SmsCounter
                );
            }

            return dt;
        }

        private static class SubscriptionColumns
        {
            public const string UserId = "userId";
            public const string SubscriptionStatusId = "subscriptionStatusId";
            public const string SubscriptionCode = "subscriptionCode";
            public const string SubscriptionId = "subscriptionId";
            public const string ProcedureTypeId = "procedureTypeId";
            public const string OrganismId = "organismId";
            public const string BundleId = "bundleId";
            public const string Freeze = "isFreeze";

            public const string Amount = "amount";
            public const string FirstName = "firstName";
            public const string LastName = "lastName";
            public const string PhoneNumber = "phoneNumber";
            public const string Whatsapp = "whatsapp";
            public const string Email = "email";

            public const string WhatsappCounter = "whatsappCounter";
            public const string TotalWhatsapp = "totalWhatsapp";
            public const string EmailCounter = "emailCounter";
            public const string TotalEmail = "totalEmail";
            public const string SmsCounter = "smsCounter";
            public const string TotalSms = "totalSms";

            public const string ActivatedChannelMask = "activatedChannelMask";
            public const string ConfirmedChannelMask = "confirmedChannelMask";
            public const string BeginDate = "beginDate";
            public const string EndDate = "endDate";
        }
    }
}