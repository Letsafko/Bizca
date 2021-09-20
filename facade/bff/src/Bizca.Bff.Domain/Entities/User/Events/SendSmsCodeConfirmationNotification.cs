namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Core.Domain;
    public sealed class SendSmsCodeConfirmationNotification : IEvent
    {
        public SendSmsCodeConfirmationNotification(string sender,
            string phoneNumber,
            string content)
        {
            PhoneNumber = phoneNumber;
            Content = content;
            Sender = sender;
        }

        public string PhoneNumber { get; }
        public string Content { get; }
        public string Sender { get; }
    }
}