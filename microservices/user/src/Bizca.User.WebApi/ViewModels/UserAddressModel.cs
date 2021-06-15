namespace Bizca.User.WebApi.ViewModels
{
    using Bizca.User.Domain.Entities.Address;
    using Newtonsoft.Json;

    /// <summary>
    ///     User address.
    /// </summary>
    public sealed class UserAddressModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="UserAddressModel"/>
        /// </summary>
        /// <param name="address">address</param>
        public UserAddressModel(Address address)
        {
            City = address.City;
            Name = address.Name;
            Street = address.Street;
            ZipCode = address.ZipCode;
            Country = address.Country?.CountryCode;
        }

        /// <summary>
        ///     Gets user address name.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; }

        /// <summary>
        ///     Gets user address street.
        /// </summary>
        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; }

        /// <summary>
        ///     Gets user address zipcode.
        /// </summary>
        [JsonProperty("zipCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ZipCode { get; }

        /// <summary>
        ///     Gets user address city.
        /// </summary>
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; }

        /// <summary>
        ///     Gets user address country.
        /// </summary>
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; }
    }
}