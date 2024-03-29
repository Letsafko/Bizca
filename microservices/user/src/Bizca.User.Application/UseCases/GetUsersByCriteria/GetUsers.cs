﻿namespace Bizca.User.Application.UseCases.GetUsersByCriteria
{
    using Bizca.Core.Domain.Country;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Channel;
    using System.Collections.Generic;

    public sealed class GetUsers
    {
        public int UserId { get; internal set; }
        public string UserCode { get; internal set; }
        public string Civility { get; internal set; }
        public string LastName { get; internal set; }
        public string FirstName { get; internal set; }
        public string BirthCity { get; internal set; }
        public string BirthDate { get; internal set; }
        public Country BirthCountry { get; internal set; }
        public string ExternalUserId { get; internal set; }
        public Address Address { get; internal set; }
        public List<Channel> Channels { get; internal set; }
        public string EconomicActivity { get; internal set; }
    }
}