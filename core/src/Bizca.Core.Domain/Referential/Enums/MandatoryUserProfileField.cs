namespace Bizca.Core.Domain.Referential.Enums
{
    using System;

    [Flags]
    public enum MandatoryUserProfileField
    {
        None = 0,
        PhoneNumber = 1,
        Whatsapp = 2,
        Email = 4,
        BirthCounty = 8,
        BirthDate = 16,
        BirthCity = 32,
        EconomicActivity = 64,
        Address = 128,
        All = PhoneNumber | Whatsapp | Email | BirthCounty | BirthDate | BirthCity | EconomicActivity | Address
    }
}