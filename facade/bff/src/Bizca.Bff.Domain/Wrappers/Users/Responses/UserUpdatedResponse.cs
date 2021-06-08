﻿namespace Bizca.Bff.Domain.Wrappers.Users.Responses
{
    using System.Collections.Generic;

    public sealed class UserUpdatedResponse
    {
        public IEnumerable<ChannelResponse> Channels { get; set; }
        public string ExternalUserId { get; set; }
        public string EconomicActivity { get; set; }
        public string FirstName { get; set; }
        public string BirthDate { get; set; }
        public string Civility { get; set; }
        public string LastName { get; set; }
    }
}