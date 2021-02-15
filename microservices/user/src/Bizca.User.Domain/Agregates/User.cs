namespace Bizca.User.Domain.Agregates
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.Exceptions;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class User : Entity, IUser
    {
        /// <inheritdoc />
        public UserCode UserCode { get; internal set; }

        /// <summary>
        ///     Gets partner identification.
        /// </summary>
        public Partner Partner { get; internal set; }

        /// <summary>
        ///     Gets external user identification.
        /// </summary>
        public ExternalUserId ExternalUserId { get; internal set; }

        /// <summary>
        ///     Gets user civility.
        /// </summary>
        public Civility Civility { get; internal set; }

        /// <summary>
        ///     Gets user birth country.
        /// </summary>
        public Country BirthCountry { get; internal set; }

        /// <summary>
        ///     Gets user economic activity.
        /// </summary>
        public EconomicActivity EconomicActivity { get; internal set; }

        /// <summary>
        ///     Gets user bith date.
        /// </summary>
        public DateTime BirthDate { get; internal set; }

        /// <summary>
        ///     Gets user birth city.
        /// </summary>
        public string BirthCity { get; internal set; }

        /// <summary>
        ///     Gets user lastname.
        /// </summary>
        public string LastName { get; internal set; }

        /// <summary>
        ///     Gets user firstname.
        /// </summary>
        public string FirstName { get; internal set; }

        private byte[] rowVersion;

        /// <summary>
        ///     Gets row version.
        /// </summary>
        internal void SetRowVersion(byte[] value)
        {
            rowVersion = value;
        }

        /// <summary>
        ///     Gets row version.
        /// </summary>
        public byte[] GetRowVersion()
        {
            return rowVersion;
        }

        /// <summary>
        ///     Gets notification channels.
        /// </summary>
        public IReadOnlyCollection<Channel> Channels => channels.ToList();
        private readonly ICollection<Channel> channels = new List<Channel>();

        /// <summary>
        ///     Gets a channel according to channel type.
        /// </summary>
        /// <param name="channelType"></param>
        public Channel GetChannel(ChannelType channelType)
        {
            Channel channel = channels.SingleOrDefault(x => x.ChannelType == channelType);
            if (channel is null)
            {
                var failure = new DomainFailure($"channel::{channelType.Code} requested for user::{ExternalUserId} does not exist.",
                    nameof(channelType),
                    typeof(ChannelDoesNotExistForUserException));

                throw new ChannelDoesNotExistForUserException(new List<DomainFailure> { failure });
            }

            return channel;
        }

        /// <summary>
        ///     Add a new code confirmation according to channel type.
        /// </summary>
        /// <param name="channelType"></param>
        public void AddNewChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation = null)
        {
            Channel channel = GetChannel(channelType);
            if(channelConfirmation is null)
            {
                string randomCode = ChannelCodeConfirmationGenerator.GetCodeConfirmation(Partner.PartnerSettings.ChannelCodeConfirmationLength);
                DateTime expirationDate = DateTime.UtcNow.AddMinutes(Partner.PartnerSettings.ChannelCodeConfirmationExpirationDelay);
                channelConfirmation = new ChannelConfirmation(randomCode, expirationDate);
            }
            channel.AddCodeConfirmation(channelConfirmation);
        }

        /// <summary>
        ///     Update a channel with a new code confirmation.
        /// </summary>
        /// <param name="channelType"></param>
        /// <param name="channelConfirmation"></param>
        public void UpdateChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation)
        {
            Channel channel = GetChannel(channelType);
            channel.AddCodeConfirmation(channelConfirmation);
        }

        /// <summary>
        ///     Add a new 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="channelType"></param>
        /// <param name="active"></param>
        /// <param name="confirmed"></param>
        public void AddChannel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            var channel = new Channel(value, channelType, active, confirmed);
            channels.Add(channel);
        }

        public void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
        {
            Channel channel = GetChannel(channelType);
            channel.UpdateChannel(channelValue, active, confirmed);
        }

    }
}