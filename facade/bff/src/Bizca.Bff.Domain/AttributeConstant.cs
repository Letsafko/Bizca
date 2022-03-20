namespace Bizca.Bff.Domain
{
    public sealed class AttributeConstant
    {
        public static class Contact
        {
            public const string FirstName = "FIRSTNAME";
            public const string LastName = "LASTNAME";
            public const string PhoneNumber = "SMS";
            public const string Email = "EMAIL";
        }

        public static class Parameter
        {
            public const string ProcedureName = "PROCEDURENAME";
            public const string ProcedureUrl = "PROCEDUREURL";
            public const string RedirectUrl = "REDIRECTURL";
        }
    }
}
