namespace Bizca.Bff.WebApi.UseCases.V1._0.GetUsers
{
    using Bizca.Core.Api.Modules.Pagination;

    /// <summary>
    ///     Gets user criteria search.
    /// </summary>
    public sealed class GetUsers : Paged
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
    }
}