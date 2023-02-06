namespace Bizca.User.Application.UseCases.CreateUser;

using Core.Domain.Cqrs.Commands;
using Core.Domain.Cqrs.Services;
using Core.Domain.Referential.Model;
using Core.Domain.Referential.Services;
using Domain.Agregates;
using Domain.Agregates.Factories;
using Domain.Agregates.Repositories;
using Domain.Entities.Address;
using Domain.Entities.Address.Factories;
using Domain.Entities.Address.Repositories;
using Domain.Entities.Channel.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
{
    private readonly IAddressFactory addressFactory;
    private readonly IAddressRepository addressRepository;
    private readonly IChannelRepository channelRepository;
    private readonly ICreateUserOutput output;
    private readonly IReferentialService referentialService;
    private readonly IUserFactory userFactory;
    private readonly IUserRepository userRepository;

    public CreateUserUseCase(ICreateUserOutput output,
        IUserFactory userFactory,
        IAddressFactory addressFactory,
        IUserRepository userRepository,
        IChannelRepository channelRepository,
        IAddressRepository addressRepository,
        IReferentialService referentialService,
        IEventService eventService)
    {
        this.referentialService = referentialService;
        this.addressRepository = addressRepository;
        this.channelRepository = channelRepository;
        this.addressFactory = addressFactory;
        this.userRepository = userRepository;
        this.userFactory = userFactory;
        this.output = output;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true)
            ;
        UserRequest userRequest = GetUserRequest(partner, request);
        var user = await userFactory.CreateAsync(userRequest) as User;

        Address address = await GetAddressAsync(partner, request);
        if (address != null)
            user.AddNewAddress(address.Street,
                address.City,
                address.ZipCode,
                address.Country,
                address.Name);

        int userId = await userRepository.SaveAsync(user);
        await channelRepository.SaveAsync(userId, user.Profile.Channels);
        await addressRepository.SaveAsync(userId, user.Profile.Addresses);

        CreateUserDto userDto = GetUserDto(user);
        output.Ok(userDto);
        return Unit.Value;
    }

    #region helpers

    private async Task<Address> GetAddressAsync(Partner partner, CreateUserCommand request)
    {
        if (request.Address is null) return null;

        var addressRequest = new AddressRequest(partner,
            request.Address.Street,
            request.Address.City,
            request.Address.ZipCode,
            request.Address.Country,
            request.Address.Name);

        return await addressFactory.CreateAsync(addressRequest);
    }

    private UserRequest GetUserRequest(Partner partner, CreateUserCommand request)
    {
        return new UserRequest
        {
            EconomicActivity =
                string.IsNullOrWhiteSpace(request.EconomicActivity)
                    ? default(int?)
                    : int.Parse(request.EconomicActivity),
            BirthDate =
                string.IsNullOrWhiteSpace(request.BirthDate)
                    ? default(DateTime?)
                    : DateTime.Parse(request.BirthDate),
            Civility = int.Parse(request.Civility),
            ExternalUserId = request.ExternalUserId,
            BirthCountry = request.BirthCountry,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            BirthCity = request.BirthCity,
            LastName = request.LastName,
            Whatsapp = request.Whatsapp,
            Email = request.Email,
            Partner = partner
        };
    }

    private CreateUserDto GetUserDto(User user)
    {
        return new CreateUserDto
        {
            Address = user.Profile.Addresses.SingleOrDefault(x => x.Active),
            EconomicActivity = user.Profile.EconomicActivity?.EconomicActivityCode,
            BirthDate = user.Profile.BirthDate?.ToString("yyyy-MM-dd"),
            ExternalUserId = user.UserIdentifier.ExternalUserId.ToString(),
            UserCode = user.UserIdentifier.PublicUserCode.ToString(),
            BirthCountry = user.Profile.BirthCountry?.CountryCode,
            Civility = user.Profile.Civility.CivilityCode,
            Channels = user.Profile.Channels?.ToList(),
            BirthCity = user.Profile.BirthCity,
            FirstName = user.Profile.FirstName,
            LastName = user.Profile.LastName
        };
    }

    #endregion
}