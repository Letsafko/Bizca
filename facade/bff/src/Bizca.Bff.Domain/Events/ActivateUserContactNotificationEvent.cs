namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using Referential.Procedure;

    public sealed class ActivateUserContactNotificationEvent : INotificationEvent
    {
        public ActivateUserContactNotificationEvent(Procedure procedure,
            string partnerCode,
            string email)
        {
            Procedure = procedure;
            PartnerCode = partnerCode;
            Email = email;
        }

        public Procedure Procedure { get; }
        public string PartnerCode { get; }
        public string Email { get; }
    }
}