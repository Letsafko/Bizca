namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Entities.ChannelConfirmation;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RegisterCodeConfirmationUseCase : ICommandHandler<RegisterCodeConfirmationCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IReferentialService referentialService;
        private readonly IRegisterCodeConfirmationOutput output;
        private readonly IChannelConfirmationFactory channelConfirmationFactory;
        private readonly IChannelConfirmationRepository channelConfirmationRepository;
        public RegisterCodeConfirmationUseCase(IUserFactory userFactory,
            IReferentialService referentialService,
            IRegisterCodeConfirmationOutput output,
            IChannelConfirmationFactory channelConfirmationFactory,
            IChannelConfirmationRepository channelConfirmationRepository)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.referentialService = referentialService;
            this.channelConfirmationFactory = channelConfirmationFactory;
            this.channelConfirmationRepository = channelConfirmationRepository;
        }

        /// <summary>
        ///     Handles registration of user channel code confirmation.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(RegisterCodeConfirmationCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound($"user::{request.ExternalUserId} does not exist.");
                return Unit.Value;
            }

            var user = response as User;
            ChannelConfirmation channelConfirmation = channelConfirmationFactory.Create(request.ExternalUserId, request.ChannelType, user.Channels.Select(x => x.ChannelType));
            bool result = await channelConfirmationRepository.AddAsync(user.Id, channelConfirmation).ConfigureAwait(false);
            if (result)
            {
                var registerDto = new RegisterCodeConfirmationDto(channelConfirmation.ChannelType.Code,
                    //channelConfirmation.ChannelValue,
                    null,
                    channelConfirmation.CodeConfirmation);

                output.Ok(registerDto);
                return Unit.Value;
            }

            return Unit.Value;
        }
    }
}