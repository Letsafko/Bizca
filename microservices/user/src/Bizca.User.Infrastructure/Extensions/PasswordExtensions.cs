namespace Bizca.User.Infrastructure.Extensions
{
    using Domain.Agregates.ValueObjects;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class PasswordExtensions
    {
        private const string typeName = "[usr].[passwords]";

        public static DataTable ToDataTable(this IEnumerable<Password> passwords, int userId)
        {
            var dt = new DataTable(typeName);
            dt.Columns.Add(PasswordColumns.UserId, typeof(int));
            dt.Columns.Add(PasswordColumns.Active, typeof(bool));
            dt.Columns.Add(PasswordColumns.SecurityStamp, typeof(string));
            dt.Columns.Add(PasswordColumns.PassswordHash, typeof(string));

            passwords?.ToList()
                .ForEach(x =>
                {
                    dt.Rows.Add
                    (
                        userId,
                        x.Active,
                        x.SecurityStamp,
                        x.PasswordHash
                    );
                });

            return dt;
        }

        private static class PasswordColumns
        {
            public const string UserId = "userId";
            public const string Active = "active";
            public const string SecurityStamp = "securityStamp";
            public const string PassswordHash = "passswordHash";
        }
    }
}