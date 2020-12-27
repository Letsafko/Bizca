namespace Bizca.User.Domain.Agregates.Users
{
    using System;

    public sealed class UserCriteria
    {
        /// <summary>
        ///     e-mail of user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     identification phone number of user.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     identification whatsapp phone number.
        /// </summary>
        public string WhatsappNumber { get; set; }

        /// <summary>
        ///     surname of user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     first name of user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     external identification of user.
        /// </summary>
        public string ExternalUserId { get; set; }

        /// <summary>
        ///     birth date of user.
        /// </summary>
        public DateTime BirthDate { get; set;  }

        /// <summary>
        ///     number of rows elements by page.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        ///     id where to start retrieving elements.
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        ///     direction of search(next or prev).
        /// </summary>
        public string Direction { get; set; } = SearchDirection.Next;
    }

    public static class SearchDirection
    {
        public const string Next = "next";
        public const string Prev = "previous";
    }
}
