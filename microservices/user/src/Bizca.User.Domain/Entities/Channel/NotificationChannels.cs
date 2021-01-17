namespace Bizca.User.Domain.Entities.Channel
{
    using System;

    [Flags]
    public enum NotificationChannels
    {
        None = 0,
        Sms = 1,
        Email = 2,
        Whatsapp = 4,
        Messenger = 8
    }
}