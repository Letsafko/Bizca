namespace Bizca.User.Domain.Agregates.Users
{
    using System;

    [Flags]
    public enum NotificationChanels
    {
        None = 0,
        Sms = 1,
        Email = 2,
        Whatsapp = 4
    }
}
