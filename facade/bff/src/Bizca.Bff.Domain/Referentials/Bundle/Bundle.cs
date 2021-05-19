namespace Bizca.Bff.Domain.Referentials.Bundle
{
    using Bizca.Bff.Domain.Referentials.Bundle.ValueObjects;
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;

    public sealed class Bundle : ValueObject
    {
        public BundleIdentifier BundleIdentifier { get; }
        public BundleSettings BundleSettings { get; }
        public Priority Priority { get; }
        public Money Price { get; }
        public Bundle(BundleIdentifier bundleIdentifier,
            BundleSettings bundleSettings,
            Priority priority,
            Money price)
        {
            BundleIdentifier = bundleIdentifier ?? throw new ArgumentNullException(nameof(bundleIdentifier));
            BundleSettings = bundleSettings ?? throw new ArgumentNullException(nameof(bundleSettings));
            Priority = priority ?? throw new ArgumentNullException(nameof(priority));
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BundleIdentifier;
            yield return BundleSettings;
            yield return Price.Amount;
            yield return Priority;
        }
    }
}
