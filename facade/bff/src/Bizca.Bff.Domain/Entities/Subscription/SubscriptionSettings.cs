namespace Bizca.Bff.Domain.Entities.Subscription
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    public sealed class SubscriptionSettings
    {
        public DateTime? BeginDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool? IsFreeze { get; private set; }
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
            int totalSms,
            DateTime? beginDate = null,
            DateTime? endDate = null,
            bool? isFreeze = null)
        {
            WhatsappCounter = !(whatsappCounter < 0) ? whatsappCounter : throw new DomainException($"{nameof(whatsappCounter)} should be greater than zero.");
            TotalWhatsapp = !(totalWhatsapp < 0) ? totalWhatsapp : throw new DomainException($"{nameof(whatsappCounter)} should be greater than zero.");

            EmailCounter = !(emailCounter < 0) ? emailCounter : throw new DomainException($"{nameof(emailCounter)} should be greater than zero.");
            TotalEmail = !(totalEmail < 0) ? totalEmail : throw new DomainException($"{nameof(totalEmail)} should be greater than zero.");

            SmsCounter = !(smsCounter < 0) ? smsCounter : throw new DomainException($"{nameof(smsCounter)} should be greater than zero.");
            TotalSms = !(totalSms < 0) ? totalSms : throw new DomainException($"{nameof(totalSms)} should be greater than zero.");

            IsFreeze = isFreeze;
            BeginDate = beginDate;
            EndDate = endDate;
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
            IsFreeze = default;
        }
        internal void Freeze()
        {
            IsFreeze = true;
        }
    }
}