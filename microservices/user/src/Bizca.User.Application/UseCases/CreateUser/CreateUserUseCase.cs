namespace Bizca.User.Application.UseCases.CreateUser
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Entities.Channel;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly ICreateUserOutput output;
        private readonly IUserRepository userRepository;
        private readonly IChannelRepository channelRepository;
        private readonly IReferentialService referentialService;
        public CreateUserUseCase(ICreateUserOutput output,
            IUserFactory userFactory,
            IUserRepository userRepository,
            IReferentialService referentialService,
            IChannelRepository channelRepository)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.userRepository = userRepository;
            this.channelRepository = channelRepository;
            this.referentialService = referentialService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            UserRequest userRequest = GetUserRequest(partner, request);
            var user = await userFactory.CreateAsync(userRequest).ConfigureAwait(false) as User;

            int userId = await userRepository.AddAsync(user).ConfigureAwait(false);
            await channelRepository.AddAsync(userId, user.Channels).ConfigureAwait(false);

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
                Channels = user.Channels?.Select(x => new Channel(x.ChannelValue, x.ChannelType, x.Active, x.Confirmed))?.ToList()
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
    }
}