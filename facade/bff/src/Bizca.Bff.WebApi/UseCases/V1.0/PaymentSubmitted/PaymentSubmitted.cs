namespace Bizca.Bff.WebApi.UseCases.V10.PaymentSubmitted
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PaymentSubmitted
    {
        /// <summary>
        /// BUndle identifier
        /// </summary>
        public string BundleId { get; set; }

        /// <summary>
        ///  Subscription reference.
        /// </summary>
        public string Reference { get; set; }
    }
}
