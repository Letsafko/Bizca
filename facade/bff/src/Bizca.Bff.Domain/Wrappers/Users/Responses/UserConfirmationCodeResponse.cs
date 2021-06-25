namespace Bizca.Bff.Domain.Wrappers.Users.Responses
{
    public sealed class UserConfirmationCodeResponse
    {
        public string ResourceId { get; set; }
        public string Resource { get; set; }
        public bool Confirmed { get; set; }
    }
}