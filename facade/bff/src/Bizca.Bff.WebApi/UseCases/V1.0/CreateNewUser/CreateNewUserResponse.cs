namespace Bizca.Bff.WebApi.UseCases.V10.CreateNewUser
{
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    internal sealed class CreateNewUserResponse
    {
        public CreateNewUserResponse(CreateNewUserDto newUserDto)
        {
            ExternalUserId = newUserDto.ExternalUserId;
            FirstName = newUserDto.FirstName;
            Civility = newUserDto.Civility;
            LastName = newUserDto.LastName;
            Channels = newUserDto.Channels;
        }

        /// <summary>
        ///     User channels.
        /// </summary>
        [Required]
        public IEnumerable<ChannelResponse> Channels { get; }

        /// <summary>
        ///  External user identifier.
        /// </summary>
        [Required]
        public string ExternalUserId { get; }

        /// <summary>
        ///     Civility.
        /// </summary>
        [Required]
        public string Civility { get; }

        /// <summary>
        ///     Firstname.
        /// </summary>
        [Required]
        public string FirstName { get; }

        /// <summary>
        ///     Lastname.
        /// </summary>
        [Required]
        public string LastName { get; }
    }
}