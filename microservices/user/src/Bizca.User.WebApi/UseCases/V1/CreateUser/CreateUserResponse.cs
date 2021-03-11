namespace Bizca.User.WebApi.UseCases.V1.CreateUser
{
    using Bizca.User.Application.UseCases.CreateUser;
    using Bizca.User.WebApi.ViewModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    ///     Create user response.
    /// </summary>
    public sealed class CreateUserResponse
    {
        /// <summary>
        ///     Creates an instance of <see cref="CreateUserResponse"/>
        /// </summary>
        /// <param name="user"></param>
        public CreateUserResponse(CreateUserDto user)
        {
            Address = user.Address == null ? default : new AddressModel(user.Address);
            Channels = user.Channels?.Select(x => new ChannelModel(x));
            EconomicActivity = user.EconomicActivity;
            ExternalUserId = user.ExternalUserId;
            BirthCountry = user.BirthCountry;
            FirstName = user.FirstName;
            BirthCity = user.BirthCity;
            BirthDate = user.BirthDate;
            LastName = user.LastName;
            Civility = user.Civility;
        }

        /// <summary>
        ///  Gets external user identifier.
        /// </summary>
        [Required]
        public string ExternalUserId { get; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        [Required]
        public string Civility { get; }

        /// <summary>
        ///  Gets user economic activity.
        /// </summary>
        public string EconomicActivity { get; }

        /// <summary>
        ///     Gets user firstname.
        /// </summary>
        [Required]
        public string FirstName { get; }

        /// <summary>
        ///     Gets user lastname.
        /// </summary>
        [Required]
        public string LastName { get; }

        /// <summary>
        ///     Gets user birth date.
        /// </summary>
        public string BirthDate { get; }

        /// <summary>
        ///     Gets user birth country.
        /// </summary>
        public string BirthCountry { get; }

        /// <summary>
        ///     Gets user birth city.
        /// </summary>
        public string BirthCity { get; }

        /// <summary>
        ///     Gets user address.
        /// </summary>
        public AddressModel Address { get; }

        /// <summary>
        ///     Gets user notification channels.
        /// </summary>
        [Required]
        public IEnumerable<ChannelModel> Channels { get; }
    }
}