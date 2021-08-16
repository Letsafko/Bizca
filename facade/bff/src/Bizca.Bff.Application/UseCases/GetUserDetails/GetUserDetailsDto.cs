namespace Bizca.Bff.Application.UseCases.GetUserDetails
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections.Generic;
    public sealed class GetUserDetailsDto
    {
        public GetUserDetailsDto(string externalUserId,
            string firstName,
            string lastName,
            string civility,
            Role role,
            IEnumerable<ChannelResponse> channels)
        {
            ExternalUserId = externalUserId;
            FirstName = firstName;
            LastName = lastName;
            Civility = civility;
            Channels = channels;
            Role = role;
        }

        public IEnumerable<ChannelResponse> Channels { get; }
        public string ExternalUserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Civility { get; }
        public Role Role { get; }
    }
}
