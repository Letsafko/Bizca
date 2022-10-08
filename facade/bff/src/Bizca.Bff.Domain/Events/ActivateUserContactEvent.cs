namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Referentials.Procedure;

    public sealed class ActivateUserContactEvent : IEvent
    {
        public ActivateUserContactEvent(Procedure procedure,
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