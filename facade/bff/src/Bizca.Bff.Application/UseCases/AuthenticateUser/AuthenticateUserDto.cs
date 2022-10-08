namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Domain.Enumerations;
    using Domain.Wrappers.Users.Responses;
    using System.Collections.Generic;

    public sealed class AuthenticateUserDto
    {
        public AuthenticateUserDto(string externalUserId,
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