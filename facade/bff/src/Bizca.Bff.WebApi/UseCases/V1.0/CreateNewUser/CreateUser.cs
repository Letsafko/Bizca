namespace Bizca.Bff.WebApi.UseCases.V10.CreateNewUser
{
    /// <summary>
    ///     Create user.
    /// </summary>
    public sealed class CreateUser
    {
        /// <summary>
        ///     Gets or sets user identifier.
        /// </summary>
        public string ExternalUserId { get; set; }

        /// <summary>
        ///     Gets or sets user phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets user firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets user lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets user civility.
        /// </summary>
        public string Civility { get; set; }


        /// <summary>
        ///     Gets or sets user whatsapp number.
        /// </summary>
        public string Whatsapp { get; set; }

        /// <summary>
        ///     Gets or sets user email.
        /// </summary>
        public string Email { get; set; }
    }
}