namespace Bizca.User.WebApi.UseCases
{
    /// <summary>
    ///     Address.
    /// </summary>
    public sealed class UserAddress
    {
        /// <summary>
        ///     Address name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Address street.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        ///     Address zip code.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        ///     Address city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Address country.
        /// </summary>
        public string Country { get; set; }
    }
}
