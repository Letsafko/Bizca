namespace Bizca.Bff.WebApi.UseCases.V1._0.FreezeSubscription
{
    /// <summary>
    ///     Subscription activation.
    /// </summary>
    public sealed class FreezeSubscription
    {
        /// <summary>
        ///     Indicates whether subscription should be activated or not. Possible values ['true', 'false']
        /// </summary>
        public string Flag { get; set; }
    }
}