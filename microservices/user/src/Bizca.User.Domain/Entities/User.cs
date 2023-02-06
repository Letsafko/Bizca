namespace Bizca.User.Domain.Agregates;

using Core.Domain.Referential.Model;
using Entities.Address;
using Entities.Channel;
using Entities.Channel.ValueObjects;
using System;
using System.Collections.Generic;
using ValueObjects;

public sealed class User
{
    public User(UserIdentifier userIdentifier, UserProfile profile)
    {
        _passwords = new List<Password>();
        UserIdentifier = userIdentifier;
        Profile = profile;
    }

    private readonly List<Password> _passwords;
    public IReadOnlyCollection<Password> Passwords => _passwords;

    public UserIdentifier UserIdentifier { get; }

    public UserProfile Profile { get; }
    

    private byte[] _rowVersion;
    internal void SetRowVersion(byte[] value)
    {
        _rowVersion = value;
    }

    public byte[] GetRowVersion() => _rowVersion;

    internal void BuildAddress(int addressId, bool active, string street, string city, string zipCode,
        Country country, string name)
    {
        var address = new Address(addressId, active, street, city, zipCode, country, name);
        Profile.AddNewAddress(address);
    }

    public void UpdateAddress(bool active, string street, string city, string zipCode, Country country, string name)
    {
        Profile.UpdateAddress(active, street, city, zipCode, country, name);
    }

    public void AddChannelCodeConfirmation(ChannelType channelType, ChannelConfirmation channelConfirmation = null)
    {
        Channel channel = Profile.GetUserChannelByChannelType(channelType);
        if (channelConfirmation is null)
        {
            string randomCode =
                ChannelCodeConfirmationGenerator.GetCodeConfirmation(UserIdentifier.Partner.ChannelCodeConfirmationLength);
            
            DateTime expirationDate =
                DateTime.UtcNow.AddMinutes(UserIdentifier.Partner.ChannelCodeConfirmationExpirationDelay);
            
            channelConfirmation = new ChannelConfirmation(randomCode, expirationDate);
        }

        channel.AddNewCodeConfirmation(channelConfirmation);
    }

    public void UpdateChannel(string channelValue, ChannelType channelType, bool active, bool confirmed)
    {
        Profile.UpdateUserChannel(channelValue, channelType, active, confirmed);
    }

    public void AddNewAddress(string street, string city, string zipCode, Country country, string name)
    {
        var address = new Address(0, true, street, city, zipCode, country, name);
        Profile.AddNewAddress(address);
    }

    public void AddChannel(string value, ChannelType channelType, bool active, bool confirmed)
    {
        var channel = new Channel(value, channelType, active, confirmed);
        Profile.AddChannel(channel);
    }

    public void ApplyConfirmationCode(ChannelType channelType, string codeConfirmation)
    {
        Profile.ApplyConfirmationCode(channelType, codeConfirmation);
    }

    internal void BuildPassword(bool active, string passwordHash, string securityStamp)
    {
        var password = new Password(active, passwordHash, securityStamp);
        _passwords.Add(password);
    }

    public Channel GetChannel(ChannelType channelType, bool throwError = true)
    {
        return Profile.GetUserChannelByChannelType(channelType, throwError);
    }

    public void AddNewPassword(string passwordHash, string securityStamp)
    {
        var newPassword = new Password(true, passwordHash, securityStamp);
        foreach (Password pwd in _passwords) pwd.Update(false);
        _passwords.Add(newPassword);
    }

    public void UpdateProfile(EconomicActivity economicActivity,
        Country birthCountry,
        Civility civility,
        DateTime? birthDate,
        string birthCity,
        string lastName,
        string firstName)
    {
        Profile.FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : Profile.FirstName;
        Profile.BirthCity = !string.IsNullOrWhiteSpace(birthCity) ? birthCity : Profile.BirthCity;
        Profile.LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : Profile.LastName;
        Profile.EconomicActivity = economicActivity ?? Profile.EconomicActivity;
        Profile.BirthCountry = birthCountry ?? Profile.BirthCountry;
        Profile.BirthDate = birthDate ?? Profile.BirthDate;
        Profile.Civility = civility ?? Profile.Civility;
    }
}