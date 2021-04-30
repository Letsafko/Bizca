namespace Bizca.Bff.Domain.Referentials.Pricing
{
    using Bizca.Bff.Domain.Referentials.Pricing.ValueObjects;
    using System;

    public sealed class Bundle
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
    }
}
