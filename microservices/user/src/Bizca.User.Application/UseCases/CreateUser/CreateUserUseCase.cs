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
            this.output = output;
            this.userFactory = userFactory;
            this.addressFactory = addressFactory;
            this.userRepository = userRepository;
            this.addressRepository = addressRepository;
            this.channelRepository = channelRepository;
            this.referentialService = referentialService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            UserRequest userRequest = GetUserRequest(partner, request);
            var user = await userFactory.CreateAsync(userRequest).ConfigureAwait(false) as User;

            Address address = await GetAddressAsync(partner, request).ConfigureAwait(false);
            if(address != null)
            {
                user.AddNewAddress(address.Street,
                        address.City,
                        address.ZipCode,
                        address.Country,
                        address.Name);
            }

            int userId = await userRepository.AddAsync(user).ConfigureAwait(false);
            await channelRepository.UpSertAsync(userId, user.Channels).ConfigureAwait(false);
            await addressRepository.UpsertAsync(userId, user.Addresses).ConfigureAwait(false);

            CreateUserDto userDto = GetUserDto(user);
            output.Ok(userDto);
            return Unit.Value;
        }

        private CreateUserDto GetUserDto(User user)
        {
            return new CreateUserDto
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                BirthCity = user.BirthCity,
                UserCode = user.UserCode.ToString(),
                Civility = user.Civility.CivilityCode,
                ExternalUserId = user.ExternalUserId.ToString(),
                BirthDate = user.BirthDate.ToString("yyyy-MM-dd"),
                BirthCountry = user.BirthCountry.CountryCode,
                EconomicActivity = user.EconomicActivity?.EconomicActivityCode,
                Channels = user.Channels?.ToList(),
                Address = user.Addresses.SingleOrDefault(x => x.Active)
            };
        }
        private UserRequest GetUserRequest(Partner partner, CreateUserCommand request)
        {
            return new UserRequest
            {
                Partner = partner,
                Email = request.Email,
                Civility = int.Parse(request.Civility),
                BirthDate = DateTime.Parse(request.BirthDate),
                BirthCity = request.BirthCity,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Whatsapp = request.Whatsapp,
                PhoneNumber = request.PhoneNumber,
                BirthCountry = request.BirthCountry,
                ExternalUserId = request.ExternalUserId,
                EconomicActivity = int.Parse(request.EconomicActivity)
            };
        }
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
    }
}