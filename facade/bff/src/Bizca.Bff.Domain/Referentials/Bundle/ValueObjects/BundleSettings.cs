namespace Bizca.Bff.Domain.Referentials.Bundle.ValueObjects
{
    using Core.Domain;
    using System.Collections.Generic;

    public sealed class BundleSettings : ValueObject
    {
        public BundleSettings(int intervalInWeeks,
            int totalWhatsapp,
            int totalEmail,
            int totalSms)
        {
            IntervalInWeeks = intervalInWeeks;
            TotalWhatsapp = totalWhatsapp;
            TotalEmail = totalEmail;
            TotalSms = totalSms;
        }

        public int IntervalInWeeks { get; }
        public int TotalWhatsapp { get; }
        public int TotalEmail { get; }
        public int TotalSms { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return IntervalInWeeks;
            yield return TotalWhatsapp;
            yield return TotalEmail;
            yield return TotalSms;
        }
    }
}