namespace Bizca.User.Application.UseCases.CreateUser
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Address.Factories;
    using Bizca.User.Domain.Entities.Address.Repositories;
    using Bizca.User.Domain.Entities.Channel.Repositories;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly ICreateUserOutput output;
        private readonly IAddressFactory addressFactory;
        private readonly IUserRepository userRepository;
        private readonly IChannelRepository channelRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IReferentialService referentialService;
        public CreateUserUseCase(ICreateUserOutput output,
            IUserFactory userFactory,
            IAddressFactory addressFactory,
            IUserRepository userRepository,
            IChannelRepository channelRepository,
            IAddressRepository addressRepository,
            IReferentialService referentialService)
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
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            UserRequest userRequest = GetUserRequest(partner, request);
            var user = await userFactory.CreateAsync(userRequest).ConfigureAwait(false) as User;

            Address address = await GetAddressAsync(partner, request).ConfigureAwait(false);
            if (address != null)
            {
                user.AddNewAddress(address.Street,
                        address.City,
                        address.ZipCode,
                        address.Country,
                        address.Name);
            }

            int userId = await userRepository.AddAsync(user).ConfigureAwait(false);
            await channelRepository.UpSertAsync(userId, user.Profile.Channels).ConfigureAwait(false);
            await addressRepository.UpsertAsync(userId, user.Profile.Addresses).ConfigureAwait(false);

            CreateUserDto userDto = GetUserDto(user);
            output.Ok(userDto);
            return Unit.Value;
        }

        #region helpers

        private async Task<Address> GetAddressAsync(Partner partner, CreateUserCommand request)
        {
            var addressRequest = new AddressRequest(partner,
                request.Address?.Street,
                request.Address?.City,
                request.Address?.ZipCode,
                request.Address?.Country,
                request.Address?.Name);

            return await addressFactory.CreateAsync(addressRequest).ConfigureAwait(false);
        }
        private UserRequest GetUserRequest(Partner partner, CreateUserCommand request)
        {
            return new UserRequest
            {
                EconomicActivity = int.Parse(request.EconomicActivity),
                BirthDate = DateTime.Parse(request.BirthDate),
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
                BirthDate = user.Profile.BirthDate.ToString("yyyy-MM-dd"),
                ExternalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                UserCode = user.UserIdentifier.UserCode.ToString(),
                BirthCountry = user.Profile.BirthCountry.CountryCode,
                Civility = user.Profile.Civility.CivilityCode,
                Channels = user.Profile.Channels?.ToList(),
                BirthCity = user.Profile.BirthCity,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName
            };
        }

        #endregion
    }
}