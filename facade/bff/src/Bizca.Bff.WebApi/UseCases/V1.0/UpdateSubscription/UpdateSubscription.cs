namespace Bizca.Bff.WebApi.UseCases.V10.UpdateSubscription
{
    /// <summary>
    ///     Update subscription.
    /// </summary>
    public sealed class UpdateSubscription
    {
        /// <summary>
        ///     Gets or sets organism code insee.
        /// </summary>
        public string CodeInsee { get; set; }

        /// <summary>
        ///     Gets or sets procedure type identifier.
        /// </summary>
        public string ProcedureTypeId { get; set; }
    }
}