namespace Bizca.User.WebApi.UseCases.V1.GetUserDetails
{
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.WebApi.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Gets user detail response.
    /// </summary>
    public sealed class GetUserDetailResponse
    {
        /// <summary>
        ///     Create instance of user detail response.
        /// </summary>
        public GetUserDetailResponse(GetUserDetail userDetail)
        {
            Channels = userDetail.Address == null ? default : userDetail.Channels?.Select(x => new ChannelModel(x));
            Address = new AddressModel(userDetail.Address);
            EconomicActivity = userDetail.EconomicActivity;
            ExternalUserId = userDetail.ExternalUserId;
            BirthCountry = userDetail.BirthCountry;
            FirstName = userDetail.FirstName;
            BirthCity = userDetail.BirthCity;
            BirthDate = userDetail.BirthDate;
            LastName = userDetail.LastName;
            Civility = userDetail.Civility;
        }

        /// <summary>
        ///  Gets external user identifier.
        /// </summary>
        public string ExternalUserId { get; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        public string Civility { get; }

        /// <summary>
        ///  Gets user economic activity.
        /// </summary>
        public string EconomicActivity { get; }

        /// <summary>
        ///     Gets user firstname.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        ///     Gets user lastname.
        /// </summary>
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
        public IEnumerable<ChannelModel> Channels { get; }
    }
}