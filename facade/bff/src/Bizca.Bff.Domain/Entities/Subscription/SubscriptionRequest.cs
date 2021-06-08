namespace Bizca.Bff.Domain.Entities.Subscription
{
    public sealed class SubscriptionRequest
    {
        /// <summary>
        ///     Gets or sets user identifier.
        /// </summary>
        public string ExternalUserId { get; set; }

        /// <summary>
        ///     Gets or sets organism code insee.
        /// </summary>
        public string CodeInsee { get; set; }

        /// <summary>
        ///     Gets or sets procedure type identifier.
        /// </summary>
        public int ProcedureTypeId { get; set; }
    }
}