using Bizca.Core.Domain.Partner;

namespace Bizca.User.Domain.Entities.Address
{
    public sealed class AddressRequest
    {
        public AddressRequest(Partner partner,
            string street,
            string city,
            string zipCode,
            string country,
            string name)
        {
            Country = country;
            ZipCode = zipCode;
            Partner = partner;
            Street = street;
            City = city;
            Name = name;
        }

        public Partner Partner { get; }
        public string Country { get; }
        public string ZipCode { get; }
        public string Street { get; }
        public string City { get; }
        public string Name { get; }
    }
}
