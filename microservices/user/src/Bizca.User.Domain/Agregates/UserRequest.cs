using Bizca.Core.Domain.Partner;
using System;

namespace Bizca.User.Domain.Agregates
{
    public sealed class UserRequest
    {
        public Partner Partner { get; set; }
        public string ExternalUserId { get; set; }
        public int? Civility { get; set; }
        public string BirthCountry { get; set; }
        public int? EconomicActivity { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthCity { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
    }
}