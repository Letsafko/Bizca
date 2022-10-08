namespace Bizca.Core.Domain.Referential.Model
{
    using System;

    public sealed class Partner : Entity
    {
        public Partner(int id, string code, string description)
        {
            Id = id;
            PartnerCode = code;
            Desciption = description;
        }

        public string PartnerCode { get; }
        public string Desciption { get; }
        public PartnerSettings Settings { get; } = new PartnerSettings();
    }

    public sealed class PartnerSettings
    {
        public FeatureFlags FeatureFlags { get; } = new FeatureFlags();
        public int ChannelCodeConfirmationExpirationDelay { get; set; } = 24 * 60;
        public int ChannelCodeConfirmationLength { get; set; } = 10;
    }

    public sealed class FeatureFlags
    {
        public MandatoryUserFlags MandatoryUserFlags { get; set; } = MandatoryUserFlags.Email;
        public MandatoryAddressFlags MandatoryAddressFlags { get; set; }
    }

    [Flags]
    public enum MandatoryUserFlags
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

    [Flags]
    public enum MandatoryAddressFlags
    {
        None = 0,
        Street = 1,
        ZipCode = 2,
        Name = 4,
        All = Street | ZipCode | Name
    }
}