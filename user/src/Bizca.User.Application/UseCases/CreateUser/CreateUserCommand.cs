namespace Bizca.User.Application.UseCases.CreateUser
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Domain;
    using System;

    public sealed class CreateUserCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public int Civility { get; set; }
        public string BirthCountry { get; set; }
        public int EconomicActivity { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCity { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }

        public Notification ModelState { get; } = new Notification();
    }
}
