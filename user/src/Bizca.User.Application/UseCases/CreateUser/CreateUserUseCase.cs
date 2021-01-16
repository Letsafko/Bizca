namespace Bizca.User.Application.UseCases.CreateUser
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Factories;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using User = Domain.Agregates.Users.User;

    public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserFactory _userFactory;
        private readonly ICreateUserOutput _output;
        private readonly IUserRepository _userRepository; 
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUserChannelRepository _userChannelRepository;
        public CreateUserUseCase(ICreateUserOutput output,
            IUserFactory userFactory,
            IUserRepository userRepository, 
            IPartnerRepository partnerRepository,
            IUserChannelRepository userChannelRepository)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _partnerRepository = partnerRepository ?? throw new ArgumentNullException(nameof(partnerRepository));
            _userChannelRepository = userChannelRepository ?? throw new ArgumentNullException(nameof(userChannelRepository));
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await _partnerRepository.GetByCodeAsync(request.PartnerCode).ConfigureAwait(false);
            if (partner is null)
            {
                request.ModelState.Add(nameof(request.PartnerCode), $"partner::{request.PartnerCode} is invalid.");
            }

            if (!request.ModelState.IsValid)
            {
                _output.Invalid(request.ModelState);
                return Unit.Value;
            }

            UserRequest userRequest = GetUserRequest(partner, request);
            Response<IUser> response = await _userFactory.CreateAsync(userRequest).ConfigureAwait(false);
            if (!response.Success)
            {
                _output.Invalid(response.ModelState);
                return Unit.Value;
            }

            bool success = true;
            var user = response.Value as User;
            int userId = await _userRepository.AddAsync(user).ConfigureAwait(false);
            success &= userId > 0 && await _userChannelRepository.AddAsync(userId, user.Channels).ConfigureAwait(false);

            if(success)
            {
                CreateUserDto userDto = GetUserDto(user);
                _output.Ok(userDto);
                return Unit.Value;
            }

            response.ModelState.Add("process", "unable to create user.");
            _output.Invalid(response.ModelState);
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
                Channels = user.Channels?.Select(x => new ChannelDto(x.ChannelValue, x.ChannelType.ToString().ToLower(), x.Active, x.Confirmed))?.ToList()
            };
        }
        private UserRequest GetUserRequest(Partner partner, CreateUserCommand request)
        {
            return new UserRequest
            {
                Partner = partner,
                Email = request.Email,
                Civility = request.Civility,
                BirthDate = request.BirthDate,
                BirthCity = request.BirthCity,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Whatsapp = request.Whatsapp,
                PhoneNumber = request.PhoneNumber,
                BirthCountry = request.BirthCountry,
                ExternalUserId = request.ExternalUserId,
                EconomicActivity = request.EconomicActivity
            };
        }
    }
}
