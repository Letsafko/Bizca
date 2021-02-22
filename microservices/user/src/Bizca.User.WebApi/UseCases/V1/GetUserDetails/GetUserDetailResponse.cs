namespace Bizca.User.WebApi.UseCases.V1.GetUserDetails
{
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.WebApi.ViewModels;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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
            UserCode = userDetail.UserCode;
            Civility = userDetail.Civility;
            LastName = userDetail.LastName;
            FirstName = userDetail.FirstName;
            BirthCity = userDetail.BirthCity;
            BirthDate = userDetail.BirthDate;
            BirthCountry = userDetail.BirthCountry;
            ExternalUserId = userDetail.ExternalUserId;
            EconomicActivity = userDetail.EconomicActivity;
            Channels = userDetail.Channels?.Select(x => new UserChannelModel(x));
            if (userDetail.Address != null)
            {
                Address = new UserAddressModel(userDetail.Address);
            }
        }

        /// <summary>
        ///     Get user code.
        /// </summary>
        [Required]
        public string UserCode { get; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        [Required]
        public string Civility { get; }

        /// <summary>
        ///     Gets user lastname.
        /// </summary>
        [Required]
        public string LastName { get; }

        /// <summary>
        ///     Gets user firstname.
        /// </summary>
        [Required]
        public string FirstName { get; }

        /// <summary>
        ///     Gets user birth city.
        /// </summary>
        [Required]
        public string BirthCity { get; }

        /// <summary>
        ///     Gets user birth date.
        /// </summary>
        [Required]
        public string BirthDate { get; }

        /// <summary>
        ///     Gets user birth country.
        /// </summary>
        [Required]
        public string BirthCountry { get; }

        /// <summary>
        ///  Gets external user identifier.
        /// </summary>
        [Required]
        public string ExternalUserId { get; }

        /// <summary>
        ///  Gets user economic activity.
        /// </summary>
        public string EconomicActivity { get; }

        /// <summary>
        ///     Gets user address.
        /// </summary>
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public UserAddressModel Address { get; }

        /// <summary>
        ///     Gets user notification channels.
        /// </summary>
        [Required]
        public IEnumerable<UserChannelModel> Channels { get; }
    }
}