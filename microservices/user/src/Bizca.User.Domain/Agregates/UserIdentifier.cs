namespace Bizca.User.Domain.Agregates
{
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Core.Domain.Referential.Model;

    public sealed class UserIdentifier
    {
        public UserIdentifier(int userId, ExternalUserId externalUserId, UserCode userCode, Partner partner)
        {
            ExternalUserId = externalUserId;
            UserCode = userCode;
            Partner = partner;
            UserId = userId;
        }

        /// <summary>
        ///     Gets external user identification.
        /// </summary>
        public ExternalUserId ExternalUserId { get; }

        /// <summary>
        ///     Gets user code.
        /// </summary>
        public UserCode UserCode { get; }

        /// <summary>
        ///     Gets partner identification.
        /// </summary>
        public Partner Partner { get; }

        /// <summary>
        ///     Gets technical user identifier.
        /// </summary>
        public int UserId { get; }
    }
}