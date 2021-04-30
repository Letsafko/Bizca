namespace Bizca.Bff.Domain.Entities.Subscription.Enumerations
{
    internal enum ChannelActivationStatus
    {
        None = 0,
        PhoneNumberActivated = 1,
        WhatsappActivated = 2,
        EmailActivated = 4,
        All = PhoneNumberActivated | WhatsappActivated | EmailActivated
    }
}