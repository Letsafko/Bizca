using System;

namespace Bizca.Core.Domain.Partner
{
    public sealed class Partner : Entity
    {
        public string PartnerCode { get; }
        public string Desciption { get; }
        public PartnerSettings Settings { get; } = new PartnerSettings();
        public Partner(int id, string code, string description)
        {
            Id = id;
            PartnerCode = code;
            Desciption = description;
        }
    }

    public sealed class PartnerSettings
    {
        public FeatureFlags FeatureFlags { get; } = new FeatureFlags();
        public int ChannelCodeConfirmationExpirationDelay { get; set; } = 10;
        public int ChannelCodeConfirmationLength { get; set; } = 10;
    }

    public sealed class FeatureFlags
    {
        public MandatoryUserFlags MandatoryUserFlags { get; set; } = MandatoryUserFlags.PhoneNumber |
                                                                     MandatoryUserFlags.Whatsapp |
                                                                     MandatoryUserFlags.Email;
        public MandatoryAddressFlags MandatoryAddressFlags { get; set; } = MandatoryAddressFlags.City |
                                                                           MandatoryAddressFlags.Street |
                                                                           MandatoryAddressFlags.ZipCode |
                                                                           MandatoryAddressFlags.Country;
    }

    [Flags]
    public enum MandatoryUserFlags
    {
        None = 0,
        PhoneNumber = 1,
        Whatsapp = 2,
        Email = 4,
        BirthCounty = 8,
        EconomicActivity = 16,
        All = PhoneNumber | Whatsapp | Email | BirthCounty | EconomicActivity
    }

    [Flags]
    public enum MandatoryAddressFlags
    {
        None = 0,
        City = 1,
        Street = 2,
        ZipCode = 4,
        Country = 8,
        Name = 16,
        All = City | Street | ZipCode | Country | Name
    }
}