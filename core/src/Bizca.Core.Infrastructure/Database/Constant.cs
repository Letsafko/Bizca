namespace Bizca.Core.Infrastructure.Database
{
    public static class Constant
    {
        public static class Table
        {
            internal static class Reference
            {
                internal const string EconomicActivity = "economicActivity";
                internal const string EmailTemplate = "emailTemplate";
                internal const string Civility = "civility";
                internal const string Country = "country";
                internal const string Partner = "partner";
            }
        }

        public static class Schema
        {
            public const string BackendForFrontend = "bff";
            public const string Notification = "ntf";
            internal const string Reference = "ref";
            public const string User = "usr";
        }
    }
}