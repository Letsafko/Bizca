namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections.Generic;
    public sealed class CreateNewUserDto
    {
        public CreateNewUserDto(string externalUserId,
            string firstName,
            string lastName,
            string civility,
            IEnumerable<ChannelResponse> channels)
        {
            ExternalUserId = externalUserId;
            FirstName = firstName;
            LastName = lastName;
            Civility = civility;
            Channels = channels;
        }

        public IEnumerable<ChannelResponse> Channels { get; }
        public string ExternalUserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Civility { get; }

    }
}