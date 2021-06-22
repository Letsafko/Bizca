namespace Bizca.Bff.Domain.Wrappers.Notification.Requests
{
    public sealed class MailAddressRequest
    {
        public MailAddressRequest(string name, string email)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; }
        public string Name { get; }
    }
}