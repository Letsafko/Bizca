namespace Bizca.User.Domain.Agregates.Users
{
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.Users.ValueObjects;
    using System;

    public sealed class User : IUser
    {
        public User(Partner partner, UserCode userCode, ExternalUserId externalUserId)
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
        public ExternalUserId ExternalUserId { get; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        public Civility Civility { get; set; }

        /// <summary>
        ///     Gets user birth country.
        /// </summary>
        public Country BirthCountry { get; set; }

        /// <summary>
        ///     Gets user economic activity.
        /// </summary>
        public EconomicActivity EconomicActivity { get; set; }

        /// <summary>
        ///     Gets user bith date.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        ///     Gets user birth city.
        /// </summary>
        public string BirthCity { get; set; }

        /// <summary>
        ///     Gets user lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets user firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets user phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets user email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets notification channels of user.
        /// </summary>
        public int Channels { get; set; }


    }
}