namespace Bizca.User.Domain.Agregates.Factories;

using BusinessCheck.UserRule;
using BusinessCheck.UserRule.Contract;
using Core.Domain;
using Core.Domain.Exceptions;
using Core.Domain.Referential.Model;
using Core.Domain.Referential.Services;
using Core.Domain.Rules;
using Core.Domain.Rules.Exception;
using Core.Domain.Rules.Extension;
using Entities.Channel;
using Entities.Channel.ValueObjects;
using Newtonsoft.Json;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValueObjects;

public sealed class UserFactory : IUserFactory
{
    private readonly IReferentialService _referentialService;
    private readonly IUserRuleEngine _userRuleEngine;
    private readonly IUserRepository _userRepository;

    public UserFactory(IUserRuleEngine userRuleEngine,
        IUserRepository userRepository,
        IReferentialService referentialService)
    {
        _referentialService = referentialService;
        _userRuleEngine = userRuleEngine;
        _userRepository = userRepository;
    }
    
    public async Task<User> BuildByPartnerAndExternalUserIdAsync(Partner partner, string externalUserId)
    {
        Dictionary<ResultName, IEnumerable<dynamic>> resultDico = await _userRepository
            .GetByPartnerIdAndExternalUserIdAsync(partner.PartnerId, externalUserId);
        
        return await ConstructUserAsync(partner, resultDico);
    }

    public async Task<User> BuildByPartnerAndChannelResourceAsync(Partner partner, string channelResource)
    {
        Dictionary<ResultName, IEnumerable<dynamic>> resultDico = await _userRepository
            .GetByPartnerIdAndChannelResourceAsync(partner.PartnerId, channelResource);
        return await ConstructUserAsync(partner, resultDico);
    }

    public async Task<User> CreateAsync(UserRequest request)
    {
        var checkResults = await _userRuleEngine.CheckRulesAsync(request);
        checkResults.ManageResultChecks();

        (EconomicActivity economicActivity, Civility civility, Country birthCountry) =
            await GetReferentialDataAsync(request.EconomicActivity,
                request.BirthCountry,
                request.Civility);

        var userIdentifier = new UserIdentifier(0,
            new ExternalUserId(request.ExternalUserId),
            new PublicUserCode(Guid.NewGuid()),
            request.Partner);

        var profile = new UserProfile(economicActivity,
            birthCountry,
            request.BirthDate,
            civility,
            request.BirthCity,
            request.FirstName,
            request.LastName);

        var user = new User(userIdentifier, profile);
        AddOrUpdateChannel(user, ChannelType.Whatsapp, request.Whatsapp);
        AddOrUpdateChannel(user, ChannelType.Sms, request.PhoneNumber);
        AddOrUpdateChannel(user, ChannelType.Email, request.Email);

        return user;
    }

    public async Task<User> UpdateAsync(UserRequest request)
    {
        var user = await BuildByPartnerAndExternalUserIdAsync(request.Partner, request.ExternalUserId);
        if(user is null) return null;

        (EconomicActivity economicActivity, Civility civility, Country birthCountry) =
            await GetReferentialDataAsync(request.EconomicActivity,
                request.BirthCountry,
                request.Civility);

        AddOrUpdateChannel(user, ChannelType.Whatsapp, request.Whatsapp);
        AddOrUpdateChannel(user, ChannelType.Sms, request.PhoneNumber);
        AddOrUpdateChannel(user, ChannelType.Email, request.Email);

        user.UpdateProfile(economicActivity,
            birthCountry,
            civility,
            request.BirthDate,
            request.BirthCity,
            request.LastName,
            request.FirstName);

        return user;
    }

    private async Task<(EconomicActivity economicActivity, Civility civility, Country birthCountry)>
        GetReferentialDataAsync(int? economicActivity, string birthCountry, int? civilityId)
    {
        var birthCountryTask = Task.FromResult<Country>(default);
        if (!string.IsNullOrWhiteSpace(birthCountry))
        {
            birthCountryTask = int.TryParse(birthCountry, out int birthCountryId)
                ? _referentialService.GetCountryByIdAsync(birthCountryId) 
                : _referentialService.GetCountryByCodeAsync(birthCountry);
        }

        var economicActivityTask = Task.FromResult<EconomicActivity>(default);
        if (economicActivity.HasValue)
            economicActivityTask = _referentialService.GetEconomicActivityByIdAsync(economicActivity.Value);

        var civilityTask = Task.FromResult<Civility>(default);
        if (civilityId.HasValue) civilityTask = _referentialService.GetCivilityByIdAsync(civilityId.Value);

        await Task.WhenAll(birthCountryTask, civilityTask, economicActivityTask);
        return
        (
            economicActivityTask.Result,
            civilityTask.Result,
            birthCountryTask.Result
        );
    }

    private async Task<User> ConstructUserAsync(Partner partner,
        IReadOnlyDictionary<ResultName, IEnumerable<dynamic>> resultSets)
    {
        dynamic result = resultSets[ResultName.User].FirstOrDefault();
        if (result is null) return null;

        (EconomicActivity economicActivity, Civility civility, Country birthCountry) =
            await GetReferentialDataAsync((int?)result.economicActivityId,
                (string)result.birthCountryId?.ToString(),
                (int?)result.civilityId);

        var userIdentifier = new UserIdentifier(result.userId,
            new ExternalUserId(result.externalUserId),
            new PublicUserCode(result.userCode),
            partner);

        var profile = new UserProfile(economicActivity,
            birthCountry,
            result.birthDate,
            civility,
            result.birthCity,
            result.firstName,
            result.lastName);

        var user = new User(userIdentifier, profile);
        user.SetRowVersion(result.rowversion);

        if (!string.IsNullOrWhiteSpace(result.whatsapp))
            user.AddChannel(result.whatsapp, ChannelType.Whatsapp, ConvertToBoolean(result.whatsappActive),
                ConvertToBoolean(result.whatsappConfirmed));

        if (!string.IsNullOrWhiteSpace(result.email))
            user.AddChannel(result.email, ChannelType.Email, ConvertToBoolean(result.emailActive),
                ConvertToBoolean(result.emailConfirmed));

        if (!string.IsNullOrWhiteSpace(result.phone))
            user.AddChannel(result.phone, ChannelType.Sms, ConvertToBoolean(result.phoneActive),
                ConvertToBoolean(result.phoneConfirmed));

        foreach (dynamic channel in resultSets[ResultName.ChannelConfirmations])
        {
            dynamic channelType = ChannelType.GetByCode(channel.channelId);
            var channelCode = new ChannelConfirmation(channel.confirmationCode, channel.expirationDate);
            user.AddChannelCodeConfirmation(channelType, channelCode);
        }

        foreach (dynamic address in resultSets[ResultName.Addresses])
        {
            var ctr = new Country(address.countryId,
                address.countryCode,
                address.description);

            user.BuildAddress(address.addressId,
                address.active,
                address.street,
                address.city,
                address.zipcode,
                ctr,
                address.name);
        }

        foreach (dynamic pwd in resultSets[ResultName.Passwords])
            user.BuildPassword(pwd.active, pwd.passwordHash, pwd.securityStamp);

        return user;
    }

    private static void AddOrUpdateChannel(User user, ChannelType channelType, string channelValue)
    {
        if (string.IsNullOrWhiteSpace(channelValue)) 
            return;
        
        var channel = user.Profile.GetUserChannelByChannelType(channelType, false);
        var channelWithSameValue = user.Profile.GetUserChannelByValue(channelValue, false);

        if (channel is null)
            user.AddChannel(channelValue,
                channelType,
                channelWithSameValue?.Active ?? false,
                channelWithSameValue?.Confirmed ?? false);
        else if (!channel.ChannelValue.Equals(channelValue, StringComparison.OrdinalIgnoreCase))
            user.UpdateChannel(channelValue,
                channelType,
                channelWithSameValue?.Active ?? false,
                channelWithSameValue?.Confirmed ?? false);
    }
    
    private static bool ConvertToBoolean(int value) => value == 1;
}