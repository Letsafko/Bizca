namespace Bizca.Bff.WebApi.ViewModels
{
    using Bizca.Bff.Application.UseCases.AuthenticateUser;
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    ///     User view model
    /// </summary>
    internal class UserViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="UserViewModel"/>
        /// </summary>
        /// <param name="newUserDto"></param>
        public UserViewModel(CreateNewUserDto newUserDto)
        {
            ExternalUserId = newUserDto.ExternalUserId;
            FirstName = newUserDto.FirstName;
            Civility = newUserDto.Civility;
            LastName = newUserDto.LastName;
            Channels = newUserDto.Channels.Select(x => new ChannelViewModel(x.ChannelValue,
                x.ChannelType,
                x.Confirmed,
                x.Active));
        }

        /// <summary>
        ///     Creates an instance of <see cref="UserViewModel"/>
        /// </summary>
        /// <param name="userDto"></param>
        public UserViewModel(AuthenticateUserDto userDto)
        {
            ExternalUserId = userDto.ExternalUserId;
            FirstName = userDto.FirstName;
            Civility = userDto.Civility;
            LastName = userDto.LastName;
            Channels = userDto.Channels.Select(x => new ChannelViewModel(x.ChannelValue,
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
