namespace Bizca.Bff.WebApi.UseCases.V1._0.PaymentExecuted
{
    /// <summary>
    ///     Payment executed confirmation
    /// </summary>
    public sealed class PaymentExecuted
    {
        /// <summary>
        ///     Subscription reference
        /// </summary>
        public string SubscriptionCode { get; set; }
    }
}