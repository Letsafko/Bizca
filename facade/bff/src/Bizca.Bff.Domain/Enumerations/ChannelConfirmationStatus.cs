namespace Bizca.Bff.Domain.Enumerations
{
    using System;

    [Flags]
    public enum ChannelConfirmationStatus
    {
        None = 0,
        PhoneNumberConfirmed = 1,
        EmailConfirmed = 2,
        WhatsappConfirmed = 4,
        MessengerConfirmed = 8,
        All = PhoneNumberConfirmed | WhatsappConfirmed | EmailConfirmed | MessengerConfirmed
    }
}