namespace Bizca.User.Application.UseCases.UpdateUser
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.Repositories;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateUserUseCase : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IUpdateUserOutput output;
        private readonly IUserRepository userRepository;
        private readonly IChannelRepository channelRepository;
        private readonly IReferentialService referentialService;
        public UpdateUserUseCase(IUpdateUserOutput output,
            IUserFactory userFactory,
            IUserRepository userRepository,
            IChannelRepository channelRepository,
            IReferentialService referentialService)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.userRepository = userRepository;
            this.channelRepository = channelRepository;
            this.referentialService = referentialService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            UserRequest userRequest = GetUserRequest(partner, request);
            IUser response = await userFactory.UpdateAsync(userRequest).ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound();
                return Unit.Value;
            }

            var user = response as User;
            await userRepository.UpdateAsync(user).ConfigureAwait(false);
            await channelRepository.UpSertAsync(user.Id, user.Channels).ConfigureAwait(false);

            UpdateUserDto userDto = GetUserDto(user);
            output.Ok(userDto);
            return Unit.Value;
        }

        private UpdateUserDto GetUserDto(User user)
        {
            return new UpdateUserDto
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
        private UserRequest GetUserRequest(Partner partner, UpdateUserCommand request)
        {
            return new UserRequest
            {
                Partner = partner,
                Email = request.Email,
                Civility = string.IsNullOrWhiteSpace(request.Civility) ? default : int.Parse(request.Civility),
                BirthDate = string.IsNullOrWhiteSpace(request.BirthDate) ? default(DateTime?) : DateTime.Parse(request.BirthDate),
                BirthCity = request.BirthCity,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Whatsapp = request.Whatsapp,
                PhoneNumber = request.PhoneNumber,
                BirthCountry = request.BirthCountry,
                ExternalUserId = request.ExternalUserId,
                EconomicActivity = string.IsNullOrWhiteSpace(request.EconomicActivity) ? default : int.Parse(request.EconomicActivity)
            };
        }
    }
}