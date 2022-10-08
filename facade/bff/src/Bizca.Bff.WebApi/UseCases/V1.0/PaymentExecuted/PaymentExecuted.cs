namespace Bizca.Bff.WebApi.UseCases.V10.PaymentExecuted
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