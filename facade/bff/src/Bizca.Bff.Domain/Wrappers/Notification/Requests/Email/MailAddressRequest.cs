namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Email
{
    public sealed class MailAddressRequest
    {
        public MailAddressRequest(string email, string name = default)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; }
        public string Name { get; }
    }
}