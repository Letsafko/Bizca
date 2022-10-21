namespace Bizca.User.Domain.Entities.Address
{
    using Core.Domain;
    using Core.Domain.Referential.Model;

    public sealed class Address : Entity
    {
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

        public string Name { get; private set; }
        public bool Active { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public Country Country { get; private set; }

        internal void Update(bool? active,
            string street,
            string city,
            string zipCode,
            Country country,
            string name)
        {
            if(!string.IsNullOrWhiteSpace(name))
                Name = name;
            
            if(!string.IsNullOrWhiteSpace(city))
                City = city;
            
            if(active.HasValue)
                Active = active.Value;
            
            if(!string.IsNullOrWhiteSpace(street))
                Street = street;
            
            if(!string.IsNullOrWhiteSpace(zipCode))
                ZipCode = zipCode;
            
            if(country != null)
                Country = country;
        }
    }
}