namespace Bizca.Bff.Domain.Enumerations
{
    using System;

    [Flags]
    public enum ChannelActivationStatus
    {
        None = 0,
        PhoneNumberActivated = 1,
        EmailActivated = 2,
        WhatsappActivated = 4,
        MessengerActivated = 8,
        All = PhoneNumberActivated | WhatsappActivated | EmailActivated | MessengerActivated
    }
}