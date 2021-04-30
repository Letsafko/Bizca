namespace Bizca.Bff.Domain.Entities.Subscription
{
    using System;
    public sealed class SubscriptionSettings
    {
        public DateTime? BeginDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsFreeze { get; private set; }
        public int WhatsappCounter { get; }
        public int EmailCounter { get; }
        public int SmsCounter { get; }
        public SubscriptionSettings(int whatsappCounter,
            int emailCounter,
            int smsCounter)
        {
            WhatsappCounter = !(whatsappCounter < 0) ? whatsappCounter : throw new ArgumentException(nameof(whatsappCounter));
            EmailCounter = !(emailCounter < 0) ? emailCounter : throw new ArgumentException(nameof(emailCounter));
            SmsCounter = !(smsCounter < 0) ? smsCounter : throw new ArgumentException(nameof(smsCounter));
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