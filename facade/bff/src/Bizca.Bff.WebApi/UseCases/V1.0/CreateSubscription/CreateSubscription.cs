namespace Bizca.Bff.WebApi.UseCases.V1._0.CreateSubscription
{
    /// <summary>
    ///     Create subscription.
    /// </summary>
    public sealed class CreateSubscription
    {
        /// <summary>
        ///     Gets or sets organism code insee.
        /// </summary>
        public string CodeInsee { get; set; }

        /// <summary>
        ///     Gets or sets procedure type identifier.
        /// </summary>
        public string ProcedureTypeId { get; set; }

        /// <summary>
        ///     Gets or sets user subscription phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets user subscription firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets user subscription lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets user subscription email.
        /// </summary>
        public string Email { get; set; }
    }
}