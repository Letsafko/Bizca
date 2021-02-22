namespace Bizca.User.Domain.Entities.Address
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Country;

    public sealed class Address : Entity
    {
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public Country Country { get; private set; }
        public Address(int id,
            bool active,
            string street,
            string city,
            string zipCode,
            Country country,
            string name)
        {
            Id = id;
            Name = name;
            City = city;
            Active = active;
            Street = street;
            ZipCode = zipCode;
            Country = country;
        }

        internal void Update(bool active,
            string street,
            string city,
            string zipCode,
            Country country,
            string name)
        {
            Name = name;
            City = city;
            Active = active;
            Street = street;
            ZipCode = zipCode;
            Country = country;
        }
    }
}