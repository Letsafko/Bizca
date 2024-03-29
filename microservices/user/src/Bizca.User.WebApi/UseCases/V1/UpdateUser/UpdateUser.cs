﻿namespace Bizca.User.WebApi.UseCases.V1.UpdateUser
{
    /// <summary>
    ///     update user input.
    /// </summary>
    public sealed class UpdateUser
    {
        /// <summary>
        ///     civility.
        /// </summary>
        public string Civility { get; set; }

        /// <summary>
        ///     birth country.
        /// </summary>
        public string BirthCountry { get; set; }

        /// <summary>
        ///     economic activity.
        /// </summary>
        public string EconomicActivity { get; set; }

        /// <summary>
        ///     birth date.
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        ///     birth city.
        /// </summary>
        public string BirthCity { get; set; }

        /// <summary>
        ///     surname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     fisrtname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     whatsapp phone number.
        /// </summary>
        public string Whatsapp { get; set; }

        /// <summary>
        ///     email.
        /// </summary>
        public string Email { get; set; }
    }
}