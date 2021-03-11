namespace Bizca.User.WebApi.ViewModels
{
    using Bizca.User.Domain.Entities.Address;

    /// <summary>
    ///     User address.
    /// </summary>
    public sealed class AddressModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="AddressModel"/>
        /// </summary>
        /// <param name="address">address</param>
        public AddressModel(Address address)
        {
            Country = address.Country.CountryCode;
            ZipCode = address.ZipCode;
            Street = address.Street;
            Name = address.Name;
            City = address.City;
        }

        /// <summary>
        ///     Gets user address name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets address street.
        /// </summary>
        public string Street { get; }

        /// <summary>
        ///     Gets address zipcode.
        /// </summary>
        public string ZipCode { get; }

        /// <summary>
        ///     Gets address city.
        /// </summary>
        public string City { get; }

        /// <summary>
        ///     Gets address country code.
        /// </summary>
        public string Country { get; }
    }
}