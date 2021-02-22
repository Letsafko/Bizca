namespace Bizca.User.Application.UseCases.CreateUser
{
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using System.Collections.Generic;

    public sealed class CreateUserDto
    {
        public string UserCode { get; set; }
        public string ExternalUserId { get; set; }
        public string Civility { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string EconomicActivity { get; set; }
        public Address Address { get; set; }
        public List<Channel> Channels { get; set; }
    }
}