﻿namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Core.Application.Commands;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using Domain;
    using Domain.Agregates;
    using Domain.Agregates.Factories;
    using Domain.Agregates.Repositories;
    using Domain.Entities.Channel;
    using Domain.Entities.Channel.Repositories;
    using Domain.Entities.Channel.ValueObjects;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RegisterCodeConfirmationUseCase : ICommandHandler<RegisterCodeConfirmationCommand>
    {
        private readonly IChannelConfirmationRepository channelConfirmationRepository;
        private readonly IRegisterCodeConfirmationOutput output;
        private readonly IReferentialService referentialService;
        private readonly IUserFactory userFactory;
        private readonly IUserRepository userRepository;

        public RegisterCodeConfirmationUseCase(IUserFactory userFactory,
            IReferentialService referentialService,
            IRegisterCodeConfirmationOutput output,
            IUserRepository userRepository,
            IChannelConfirmationRepository channelConfirmationRepository)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.userRepository = userRepository;
            this.referentialService = referentialService;
            this.channelConfirmationRepository = channelConfirmationRepository;
        }

        /// <summary>
        ///     Handles registration of user channel code confirmation.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(RegisterCodeConfirmationCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true)
                .ConfigureAwait(false);
            IUser response = await userFactory.BuildByPartnerAndExternalUserIdAsync(partner, request.ExternalUserId)
                .ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound($"no user associated to '{request.ExternalUserId}' exists.");
                return Unit.Value;
            }

            var user = response as User;
            user.AddChannelCodeConfirmation(request.ChannelType);
            await userRepository.UpdateAsync(user).ConfigureAwait(false);

            IReadOnlyCollection<ChannelConfirmation> channelCodes = user.GetChannel(request.ChannelType).ChannelCodes;
            await channelConfirmationRepository
                .UpsertAsync(user.UserIdentifier.UserId, request.ChannelType, channelCodes).ConfigureAwait(false);

            RegisterCodeConfirmationDto registerCode = GetChannel(request.ChannelType, user);
            output.Ok(registerCode);
            return Unit.Value;
        }

        #region helpers

        private RegisterCodeConfirmationDto GetChannel(ChannelType channelType, User user)
        {
            Channel channel = user.GetChannel(channelType);
            return new RegisterCodeConfirmationDto(channel.ChannelType.Description,
                channel.ChannelValue,
                channel.ChannelCodes.OrderByDescending(x => x.ExpirationDate).First().CodeConfirmation);
        }

        #endregion
    }
}