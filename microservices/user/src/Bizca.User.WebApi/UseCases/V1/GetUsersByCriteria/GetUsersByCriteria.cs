namespace Bizca.User.WebApi.UseCases.V1.GetUsersByCriteria
{
    using Core.Api.Modules.Pagination;
    using System;

    /// <summary>
    ///     Gets user criteria search.
    /// </summary>
    public sealed class GetUsersByCriteria : Paged, ICloneable
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
        ///     Gets a copy of current object.
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}