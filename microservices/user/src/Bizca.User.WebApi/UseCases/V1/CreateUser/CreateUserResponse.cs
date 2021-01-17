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
            UserCode = user.UserCode;
            Civility = user.Civility;
            LastName = user.LastName;
            FirstName = user.FirstName;
            BirthCity = user.BirthCity;
            BirthDate = user.BirthDate;
            BirthCountry = user.BirthCountry;
            ExternalUserId = user.ExternalUserId;
            EconomicActivity = user.EconomicActivity;
            Channels = user.Channels?.Select(x => new UserChannelModel(x));
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
        ///     Gets user notification channels.
        /// </summary>
        [Required]
        public IEnumerable<UserChannelModel> Channels { get; }
    }
}