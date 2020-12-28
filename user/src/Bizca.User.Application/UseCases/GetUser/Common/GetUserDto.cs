namespace Bizca.User.Application.UseCases.GetUser.Common
{
    using System.Collections.Generic;

    public sealed class GetUserDto
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string ExternalUserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Civility { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<ChannelDto> Channels { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string EconomicActivity { get; set; }
    }
}
