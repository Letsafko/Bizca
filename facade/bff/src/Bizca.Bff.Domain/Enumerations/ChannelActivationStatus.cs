namespace Bizca.Bff.Domain.Enumerations
{
    using System;

    [Flags]
    public enum ChannelActivationStatus
    {
        None = 0,
        PhoneNumberActivated = 1,
        WhatsappActivated = 2,
        EmailActivated = 4,
        All = PhoneNumberActivated | WhatsappActivated | EmailActivated
    }
}