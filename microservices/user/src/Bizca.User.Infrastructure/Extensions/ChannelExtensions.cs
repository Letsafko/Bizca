namespace Bizca.User.Infrastructure.Extensions
{
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class ChannelExtensions
    {
        public static DataTable ToDataTable(this IEnumerable<ChannelConfirmation> channelConfirmations, int userId, int channelId, string typeName)
        {
            var dt = new DataTable(typeName);
            dt.Columns.Add(ChannelColumns.UserId, typeof(int));
            dt.Columns.Add(ChannelColumns.ChannelId, typeof(int));
            dt.Columns.Add(ChannelColumns.ConfirmationCode, typeof(string));
            dt.Columns.Add(ChannelColumns.CodeExpirationDate, typeof(DateTime));
            channelConfirmations
                ?.ToList()
                .ForEach(x =>
                {
                    dt.Rows.Add
                    (
                        userId,
                        channelId,
                        x.CodeConfirmation,
                        x.ExpirationDate
                    );
                });

            return dt;
        }

        public static DataTable ToDataTable(this IEnumerable<Channel> channels, int userId, string typeName)
        {
            var dt = new DataTable(typeName);
            dt.Columns.Add(ChannelColumns.UserId, typeof(int));
            dt.Columns.Add(ChannelColumns.ChannelId, typeof(int));
            dt.Columns.Add(ChannelColumns.Active, typeof(bool));
            dt.Columns.Add(ChannelColumns.Confirmed, typeof(bool));
            dt.Columns.Add(ChannelColumns.ChannelValue, typeof(string));

            channels?.ToList()
                .ForEach(x =>
                {
                    dt.Rows.Add
                    (
                        userId,
                        x.ChannelType.Id,
                        x.Active,
                        x.Confirmed,
                        x.ChannelValue
                    );
                });

            return dt;
        }

        private static class ChannelColumns
        {
            public const string UserId = "userId";
            public const string Active = "active";
            public const string ChannelValue = "value";
            public const string ChannelId = "channelId";
            public const string Confirmed = "confirmed";
            public const string ConfirmationCode = "confirmationCode";
            public const string CodeExpirationDate = "expirationDate";
        }
    }
}