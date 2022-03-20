﻿namespace Bizca.Bff.Domain
{
    public sealed class AttributeConstant
    {
        public static class Contact
        {
            public const string FirstName = "FIRSTNAME";
            public const string LastName = "LASTNAME";
            public const string Civility = "CIVILITY";
            public const string PhoneNumber = "SMS";
            public const string Email = "EMAIL";
        }

        public static class Parameter
        {
            public const string ActivateUserUrl = "ACTIVATE_USER_URL";
            public const string ProcedureName = "PROCEDURE_NAME";
            public const string ProcedureUrl = "PROCEDURE_URL";
        }
    }
}
