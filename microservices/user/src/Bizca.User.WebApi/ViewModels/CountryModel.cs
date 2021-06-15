namespace Bizca.User.WebApi.ViewModels
{
    /// <summary>
    ///     Country model.
    /// </summary>
    public sealed class CountryModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="CountryModel"/>
        /// </summary>
        /// <param name="countryCode">country code.</param>
        /// <param name="description">country description.</param>
        public CountryModel(string countryCode, string description)
        {
            Description = description;
            CountryCode = countryCode;
        }

        /// <summary>
        ///     Gets country code.
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        ///     Gets country description.
        /// </summary>
        public string Description { get; }
    }
}