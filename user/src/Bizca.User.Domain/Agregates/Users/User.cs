namespace Bizca.User.Domain.Agregates.Users
{
    public sealed class User : IUser
    {
        public User(UserCode userId, string externalUserId, string partnerCode)
        {
            UserCode = userId;
            PartnerCode = partnerCode;
            ExternalUserId = externalUserId;
        }

        /// <inheritdoc />
        public UserCode UserCode { get; }

        /// <summary>
        ///     Gets partner reference code
        /// </summary>
        public string PartnerCode { get; }

        /// <summary>
        ///     Gets external user identification
        /// </summary>
        public string ExternalUserId { get; }
    }
}