namespace Bizca.User.WebApi.UseCases.V1.GetUsers
{
    /// <summary>
    ///     Gets user criteria search.
    /// </summary>
    public sealed class GetUsers
    {
        /// <summary>
        ///     email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     whatsapp phone number.
        /// </summary>
        public string Whatsapp { get; set; }

        /// <summary>
        ///     lastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     firstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     identifier of user.
        /// </summary>
        public string ExternalUserId { get; set; }

        /// <summary>
        ///     birthdate.
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        ///     page index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     page size.
        /// </summary>
        public string PageSize { get; set; } = "50";

        /// <summary>
        ///     page number.
        /// </summary>
        public string PageNumber { get; set; } = "1";

        /// <summary>
        ///     direction(next or previous).
        /// </summary>
        public string Direction { get; set; } = "next";
    }
}
