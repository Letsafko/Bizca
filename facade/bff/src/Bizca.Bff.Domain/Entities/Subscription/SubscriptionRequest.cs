namespace Bizca.Bff.Domain.Entities.Subscription
{
    public sealed class SubscriptionRequest
    {
        public SubscriptionRequest(string externalUserId,
            string codeInsee,
            int procedureTypeId,
            int bundleId,
            string firstName,
            string lastName,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            ExternalUserId = externalUserId;
            ProcedureTypeId = procedureTypeId;
            CodeInsee = codeInsee;
            BundleId = bundleId;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Whatsapp = whatsapp;
            Email = email;
        }

        /// <summary>
        ///     Gets or sets user identifier.
        /// </summary>
        public string ExternalUserId { get; }

        /// <summary>
        ///     Gets or sets organism code insee.
        /// </summary>
        public string CodeInsee { get; }

        /// <summary>
        ///     Gets or sets procedure type identifier.
        /// </summary>
        public int ProcedureTypeId { get; }

        /// <summary>
        ///     Gets or sets bundle identifier.
        /// </summary>
        public int BundleId { get; }

        /// <summary>
        ///     Gets or sets user subscription phone number.
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        ///     Gets or sets user subscription firstname.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        ///     Gets or sets user subscription lastname.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        ///     Gets or sets user subscription whatsapp number.
        /// </summary>
        public string Whatsapp { get; }

        /// <summary>
        ///     Gets or sets user subscription email.
        /// </summary>
        public string Email { get; }
    }
}