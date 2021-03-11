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
            this.referentialService = referentialService;
            this.channelRepository = channelRepository;
            this.userRepository = userRepository;
            this.userFactory = userFactory;
            this.output = output;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            UserRequest userRequest = GetUserRequest(partner, request);
            IUser response = await userFactory.UpdateAsync(userRequest).ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound($"no user associated to '{request.ExternalUserId}' exists");
                return Unit.Value;
            }

            var user = response as User;
            await userRepository.UpdateAsync(user).ConfigureAwait(false);
            await channelRepository.UpSertAsync(user.UserIdentifier.UserId, user.Profile.Channels).ConfigureAwait(false);

            UpdateUserDto userDto = GetUserDto(user);
            output.Ok(userDto);
            return Unit.Value;
        }

        #region helpers

        private UserRequest GetUserRequest(Partner partner, UpdateUserCommand request)
        {
            return new UserRequest
            {
                EconomicActivity = string.IsNullOrWhiteSpace(request.EconomicActivity) ? default(int?) : int.Parse(request.EconomicActivity),
                BirthDate = string.IsNullOrWhiteSpace(request.BirthDate) ? default(DateTime?) : DateTime.Parse(request.BirthDate),
                Civility = string.IsNullOrWhiteSpace(request.Civility) ? default(int?) : int.Parse(request.Civility),
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
        private UpdateUserDto GetUserDto(User user)
        {
            return new UpdateUserDto
            {
                Channels = user.Profile.Channels?.Select(x => new Channel(x.ChannelValue, x.ChannelType, x.Active, x.Confirmed))?.ToList(),
                EconomicActivity = user.Profile.EconomicActivity?.EconomicActivityCode,
                ExternalUserId = user.UserIdentifier.ExternalUserId.ToString(),
                BirthDate = user.Profile.BirthDate?.ToString("yyyy-MM-dd"),
                UserCode = user.UserIdentifier.UserCode.ToString(),
                BirthCountry = user.Profile.BirthCountry.CountryCode,
                Civility = user.Profile.Civility.CivilityCode,
                BirthCity = user.Profile.BirthCity,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName,
            };
        }

        #endregion
    }
}