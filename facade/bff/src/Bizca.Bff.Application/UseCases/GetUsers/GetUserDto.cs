namespace Bizca.Bff.Application.UseCases.GetUsers
{
    using Domain.Wrappers.Users.Responses;
    using System.Collections.Generic;

    public sealed class GetUserDto
    {
        public GetUserDto(UserResponse response)
        {
            ExternalUserId = response.ExternalUserId;
            FirstName = response.FirstName;
            LastName = response.LastName;
            Civility = response.Civility;
            Channels = response.Channels;
        }

        public IEnumerable<ChannelResponse> Channels { get; }
        public string ExternalUserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Civility { get; }
    }
}