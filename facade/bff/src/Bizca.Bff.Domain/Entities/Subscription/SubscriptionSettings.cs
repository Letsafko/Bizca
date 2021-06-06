namespace Bizca.Bff.Domain.Entities.Subscription
{
    using System;
    public sealed class SubscriptionSettings
    {
        public DateTime? BeginDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsFreeze { get; private set; }
        public int TotalWhatsapp { get; }
        public int TotalEmail { get; }
        public int TotalSms { get; }
        public int WhatsappCounter { get; }
        public int EmailCounter { get; }
        public int SmsCounter { get; }
        public SubscriptionSettings(int whatsappCounter,
            int emailCounter,
            int smsCounter,
            int totalWhatsapp,
            int totalEmail,
            int totalSms)
        {
            WhatsappCounter = !(whatsappCounter < 0) ? whatsappCounter : throw new ArgumentException(nameof(whatsappCounter));
            TotalWhatsapp = !(totalWhatsapp < 0) ? totalWhatsapp : throw new ArgumentException(nameof(totalWhatsapp));

            EmailCounter = !(emailCounter < 0) ? emailCounter : throw new ArgumentException(nameof(emailCounter));
            TotalEmail = !(totalEmail < 0) ? totalEmail : throw new ArgumentException(nameof(totalEmail));

            SmsCounter = !(smsCounter < 0) ? smsCounter : throw new ArgumentException(nameof(smsCounter));
            TotalSms = !(totalSms < 0) ? totalSms : throw new ArgumentException(nameof(totalSms));
        }

        internal void SetBeginDate(DateTime beginDate)
        {
            BeginDate = beginDate;
        }
        internal void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }
        internal void UnFreeze()
        {
            IsFreeze = false;
        }
        internal void Freeze()
        {
            IsFreeze = true;
        }
    }
}