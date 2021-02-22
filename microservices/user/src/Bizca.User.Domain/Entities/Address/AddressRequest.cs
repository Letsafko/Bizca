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
            City = city;
            Name = name;
            Street = street;
            Partner = partner;
            ZipCode = zipCode;
            Country = country;
        }

        public Partner Partner { get; }
        public string Name { get; }
        public string Street { get; }
        public string ZipCode { get; }
        public string City { get; }
        public string Country { get; }
    }
}
