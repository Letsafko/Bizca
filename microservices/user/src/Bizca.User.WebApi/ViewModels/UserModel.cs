namespace Bizca.User.WebApi.ViewModels
{
    using Application.UseCases.GetUsersByCriteria;
    using Domain.Agregates;
    using Domain.Entities.Address;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    ///     Gets user model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        ///     Create instance of user model.
        /// </summary>
        public UserModel(GetUsers user)
        {
            Channels = user.Channels.Count == 0 ? default : user.Channels.Select(x => new ChannelModel(x));
            Address = user.Address == null ? default : new AddressModel(user.Address);
            BirthCountry = user.BirthCountry?.CountryCode;
            EconomicActivity = user.EconomicActivity;
            ExternalUserId = user.ExternalUserId;
            FirstName = user.FirstName;
            BirthCity = user.BirthCity;
            BirthDate = user.BirthDate;
            LastName = user.LastName;
            Civility = user.Civility;
        }

        /// <summary>
        ///     Creates an instance of <see cref="UserModel" />
        /// </summary>
        /// <param name="user"></param>
        public UserModel(User user)
        {
            Address address = user.Profile.Addresses.SingleOrDefault(x => x.Active);
            Channels = user.Profile.Channels.Count == 0
                ? default
                : user.Profile.Channels.Select(x => new ChannelModel(x));
            Address = address is null ? default : new AddressModel(address);
            EconomicActivity = user.Profile.EconomicActivity?.EconomicActivityCode;
            ExternalUserId = user.UserIdentifier.ExternalUserId.ToString();
            BirthCountry = user.Profile.BirthCountry?.CountryCode;
            Civility = user.Profile.Civility.CivilityCode;
            BirthDate = user.Profile.BirthDate?.ToString();
            FirstName = user.Profile.FirstName;
            BirthCity = user.Profile.BirthCity;
            LastName = user.Profile.LastName;
        }

        /// <summary>
        ///     Gets external user identifier.
        /// </summary>
        [Required]
        public string ExternalUserId { get; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        [Required]
        public string Civility { get; }

        /// <summary>
        ///     Gets user economic activity.
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