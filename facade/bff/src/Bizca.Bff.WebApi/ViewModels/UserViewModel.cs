namespace Bizca.Bff.WebApi.ViewModels
{
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    internal sealed class UserViewModel
    {
        public UserViewModel(CreateNewUserDto newUserDto)
        {
            ExternalUserId = newUserDto.ExternalUserId;
            FirstName = newUserDto.FirstName;
            Civility = newUserDto.Civility;
            LastName = newUserDto.LastName;
            Channels = newUserDto.Channels.Select(x => new ChannelViewModel(x.ChannelType,
                x.ChannelType,
                x.Confirmed,
                x.Active));
        }

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

        /// <summary>
        ///     User channels.
        /// </summary>
        [Required]
        public IEnumerable<ChannelViewModel> Channels { get; }
    }
}
