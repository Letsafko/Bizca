namespace Bizca.User.Infrastructure.Extensions
{
    using Bizca.User.Domain.Entities.Address;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class AddressExtensions
    {
        private const string typeName = "[usr].[addresses]";
        public static DataTable ToDataTable(this IEnumerable<Address> addresses, int userId)
        {
            var dt = new DataTable(typeName);
            dt.Columns.Add(AddressColumns.UserId, typeof(int));
            dt.Columns.Add(AddressColumns.AddressId, typeof(int));
            dt.Columns.Add(AddressColumns.Active, typeof(bool));
            dt.Columns.Add(AddressColumns.AddressName, typeof(string));
            dt.Columns.Add(AddressColumns.City, typeof(string));
            dt.Columns.Add(AddressColumns.ZipCode, typeof(string));
            dt.Columns.Add(AddressColumns.Street, typeof(string));
            dt.Columns.Add(AddressColumns.CountryId, typeof(int));

            addresses?.ToList()
                .ForEach(x =>
                {
                    dt.Rows.Add
                    (
                        userId,
                        x.Id,
                        x.Active,
                        x.Name,
                        x.City,
                        x.ZipCode,
                        x.Street,
                        x.Country.Id
                    );
                });

            return dt;
        }

        private static class AddressColumns
        {
            public const string City = "city";
            public const string UserId = "userId";
            public const string Active = "active";
            public const string Street = "street";
            public const string ZipCode = "zipcode";
            public const string AddressId = "addressId";
            public const string CountryId = "countryId";
            public const string AddressName = "addressName";
        }
    }
}