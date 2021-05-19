namespace Bizca.Bff.Domain.Enumerations
{
    using System;

    [Flags]
    public enum ChannelConfirmationStatus
    {
        None = 0,
        PhoneNumberConfirmed = 1,
        WhatsappConfirmed = 2,
        EmailConfirmed = 4,
        All = PhoneNumberConfirmed | WhatsappConfirmed | EmailConfirmed
    }
}