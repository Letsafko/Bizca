namespace Bizca.User.Domain.Entities.Channel
{
    using Core.Domain.Exceptions;
    using System.Collections.Generic;
    using System.Linq;
    using ValueObjects;

    public sealed class Channel
    {
        private readonly HashSet<ChannelConfirmation> _channelCodes;

        public Channel(string value, ChannelType channelType, bool active, bool confirmed)
        {
            _channelCodes = new HashSet<ChannelConfirmation>();
            ChannelType = channelType;
            Confirmed = confirmed;
            ChannelValue = value;
            Active = active;
        }

        public IReadOnlyList<ChannelConfirmation> ChannelCodes => _channelCodes.ToList();
        public string ChannelValue { get; private set; }
        public bool Confirmed { get; private set; }
        public bool Active { get; private set; }
        public ChannelType ChannelType { get; }
        
        internal ChannelConfirmation GetChannelConfirmation(string channelCodeConfirmation)
        {
            return _channelCodes
                       .SingleOrDefault(x => x.CodeConfirmation == channelCodeConfirmation)
                   ?? throw new ResourceNotFoundException($"channel confirmation code {channelCodeConfirmation} does not exist.");
        }
    
        internal void AddNewCodeConfirmation(ChannelConfirmation channelConfirmation)
        {
            if (!_channelCodes.Contains(channelConfirmation))
            {
                _channelCodes.Add(channelConfirmation);
            }
        }

        internal void ConfirmChannel() => Confirmed = true;
        internal void ActivateChannel() => Active = true;
        
        

        internal void UpdateChannel(string channelValue, bool channelActive, bool channelConfirmed)
        {
            Confirmed = channelConfirmed;
            ChannelValue = channelValue;
            Active = channelActive;
        }
    }
}