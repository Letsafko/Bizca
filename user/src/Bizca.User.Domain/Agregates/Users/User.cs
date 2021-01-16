namespace Bizca.User.Domain.Agregates.Users
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.Users.ValueObjects;
    using System;
    using System.Collections.Generic;

    public sealed class User : Entity, IUser
    {
        /// <inheritdoc />
        public UserCode UserCode { get; set; }

        /// <summary>
        ///     Gets partner identification.
        /// </summary>
        public Partner Partner { get; set; }

        /// <summary>
        ///     Gets external user identification.
        /// </summary>
        public ExternalUserId ExternalUserId { get; set; }

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
        ///     Gets notification channels.
        /// </summary>
        public ICollection<Channel> Channels { get; } = new List<Channel>();
    }
}