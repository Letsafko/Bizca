namespace Bizca.Core.Domain.Referential.Enums
{
    using System;

    [Flags]
    public enum MandatoryAddressField
    {
        None = 0,
        Street = 1,
        ZipCode = 2,
        Name = 4,
        Country = 8,
        All = Street | ZipCode | Name | Country
    }
}