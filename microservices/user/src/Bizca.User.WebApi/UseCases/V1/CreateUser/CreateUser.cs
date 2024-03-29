﻿namespace Bizca.User.WebApi.UseCases.V1.CreateUser
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    ///     create user input.
    /// </summary>
    public sealed class CreateUser
    {
        /// <summary>
        ///     user identifier.
        /// </summary>
        [Required]
        public string ExternalUserId { get; set; }

        /// <summary>
        ///     civility.
        /// </summary>
        //[Required]
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
        [Required]
        public string LastName { get; set; }

        /// <summary>
        ///     fisrtname.
        /// </summary>
        [Required]
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
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///     User address.
        /// </summary>
        public UserAddress Address { get; set; }
    }
}