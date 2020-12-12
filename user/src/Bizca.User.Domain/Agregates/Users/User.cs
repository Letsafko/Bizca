namespace Bizca.User.Domain.Agregates.Users
{
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.Users.ValueObjects;
    public sealed class User : IUser
    {
        public User(UserCode userCode, Partner partner, string externalUserId)
        {
            Partner = partner;
            UserCode = userCode;
            ExternalUserId = externalUserId;
        }

        /// <inheritdoc />
        public UserCode UserCode { get; }

        /// <summary>
        ///     Gets partner identification.
        /// </summary>
        public Partner Partner { get; }

        /// <summary>
        ///     Gets external user identification.
        /// </summary>
        public string ExternalUserId { get; }

        /// <summary>
        ///     Gets user lastname.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        ///     Gets user firstname.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        ///     Gets user phone number.
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        ///     Gets user email.
        /// </summary>
        public string Email { get; }
    }
}