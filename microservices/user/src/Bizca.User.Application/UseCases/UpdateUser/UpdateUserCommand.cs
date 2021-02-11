namespace Bizca.User.Application.UseCases.UpdateUser
{
    using Bizca.Core.Application.Commands;

    public sealed class UpdateUserCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public string Civility { get; set; }
        public string BirthCountry { get; set; }
        public string EconomicActivity { get; set; }
        public string BirthDate { get; set; }
        public string BirthCity { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
    }
}