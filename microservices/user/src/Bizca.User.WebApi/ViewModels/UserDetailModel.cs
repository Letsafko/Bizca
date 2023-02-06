namespace Bizca.User.WebApi.ViewModels
{
    using Application.UseCases.GetUserDetail;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    ///     Gets user detail model.
    /// </summary>
    public sealed class UserDetailModel
    {
        /// <summary>
        ///     Create instance of user model.
        /// </summary>
        public UserDetailModel(GetUserDetail user)
        {
            Channels = user.Channels.Count == 0 ? default : user.Channels.Select(x => new ChannelModel(x));
            Address = user.Address == null ? default : new AddressModel(user.Address);
            EconomicActivity = user.EconomicActivity;
            ExternalUserId = user.ExternalUserId;
            BirthCountry = user.BirthCountry;
            BirthDate = user.BirthDate;
            FirstName = user.FirstName;
            BirthCity = user.BirthCity;
            LastName = user.LastName;
            Civility = user.Civility;
        }

        /// <summary>
        ///     Gets user notification channels.
        /// </summary>
        public IEnumerable<ChannelModel> Channels { get; }

        /// <summary>
        ///     Gets user address.
        /// </summary>
        public AddressModel Address { get; }

        /// <summary>
        ///     Gets user economic activity.
        /// </summary>
        public string EconomicActivity { get; }

        /// <summary>
        ///     Gets user identifier of partner.
        /// </summary>
        [Required]
        public string ExternalUserId { get; }

        /// <summary>
        ///     Gets user birth country.
        /// </summary>
        public string BirthCountry { get; }

        /// <summary>
        ///     Gets user birth date.
        /// </summary>
        public string BirthDate { get; }

        /// <summary>
        ///     Gets user birth city.
        /// </summary>
        public string BirthCity { get; }

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
        ///     Gets user civility.
        /// </summary>
        [Required]
        public string Civility { get; }
    }
}