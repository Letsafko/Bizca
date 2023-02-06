namespace Bizca.User.Domain.Agregates;

using Core.Domain.Exceptions;
using Core.Domain.Referential.Model;
using Entities.Address;
using Entities.Channel;
using Entities.Channel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class UserProfile
{
    private readonly List<Address> _addresses;
    private readonly List<Channel> _channels;

    public UserProfile(EconomicActivity economicActivity,
        Country birthCountry,
        DateTime? birthDate,
        Civility civility,
        string birthCity,
        string firstName,
        string lastName)
    {
        _addresses = new List<Address>();
        _channels = new List<Channel>();

        EconomicActivity = economicActivity;
        BirthCountry = birthCountry;
        BirthDate = birthDate;
        BirthCity = birthCity;
        FirstName = firstName;
        LastName = lastName;
        Civility = civility;
    }

    public IReadOnlyList<Channel> Channels => _channels;

    public IReadOnlyList<Address> Addresses => _addresses;

    public EconomicActivity EconomicActivity { get; }

    public Country BirthCountry { get; }

    public DateTime? BirthDate { get; }

    public Civility Civility { get; }

    public string BirthCity { get; }

    public string FirstName { get; }
    
    public string LastName { get; }
    
    internal void UpdateAddress(bool active, 
        string street, 
        string city, 
        string zipCode, 
        Country country,
        string name)
    {
        var address = GetActiveAddress();
        address.Update(active, street, city, zipCode, country, name);
    }
    
    internal void AddNewAddress(Address newAddress)
    {
        foreach (var address in _addresses)
            address.DisableAddress();
        
        _addresses.Add(newAddress);
    }

    internal void ActivateUserChannel(ChannelType channelType)
    {
        var channel = GetUserChannelByChannelType(channelType);
        channel.ActivateChannel();
    }
    
    internal void ApplyConfirmationCode(ChannelType channelType, string codeConfirmation)
    {
        var channel = GetUserChannelByChannelType(channelType);
        var channelConfirmation = channel.GetChannelConfirmation(codeConfirmation);
        var isConfirmationCodeExpired = channelConfirmation.IsConfirmationCodeExpired(DateTime.UtcNow);
        if (isConfirmationCodeExpired)
            throw new ChannelCodeConfirmationHasExpiredUserException(
                $"confirmation code {codeConfirmation} for {channelType.Label} channel already expired.");

        channel.ConfirmChannel();
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    

    

    

    
    

    
    
    
    internal void UpdateUserChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
    {
        var channel = GetUserChannelByChannelType(channelType);
        channel.UpdateChannel(channelValue, active, confirmed);
    }

    internal Channel GetUserChannelByChannelType(ChannelType channelType, bool throwError = true)
    {
        var channel = _channels.SingleOrDefault(x => x.ChannelType == channelType);
        if (channel is not null || !throwError) 
            return channel;
        
        throw new ResourceNotFoundException($"requested channel {channelType.Label} does not exist.");
    }

    internal Channel GetUserChannelByValue(string channelValue, bool throwError = true)
    {
        var channel = _channels.SingleOrDefault(
            x => x.ChannelValue.Equals(channelValue, StringComparison.OrdinalIgnoreCase));

        if (channel is not null || !throwError) return channel;
        throw new ResourceNotFoundException($"requested channel {channelValue} does not exist.");
    }

    

    internal void AddChannel(Channel channel) => _channels.Add(channel);

    
    private Address GetActiveAddress() => _addresses.Single(x => x.Active);
}