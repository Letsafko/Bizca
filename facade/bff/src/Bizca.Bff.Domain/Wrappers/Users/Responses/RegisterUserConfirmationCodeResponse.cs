namespace Bizca.Bff.Domain.Wrappers.Users.Responses
{
    public sealed class RegisterUserConfirmationCodeResponse
    {
        public string ConfirmationCode { get; set; }
        public string ResourceId { get; set; }
        public string Resource { get; set; }
    }
}