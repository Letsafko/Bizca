namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation;

using Core.Domain.Cqrs.Commands;
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
    private readonly IRegisterCodeConfirmationOutput _registerCodeConfirmationOutput;
    private readonly IChannelConfirmationRepository _channelConfirmationRepository;
    private readonly IReferentialService _referentialService;
    private readonly IUserRepository _userRepository;
    private readonly IUserFactory _userFactory;

    public RegisterCodeConfirmationUseCase(IUserFactory userFactory,
        IReferentialService referentialService,
        IRegisterCodeConfirmationOutput registerCodeConfirmationOutput,
        IUserRepository userRepository,
        IChannelConfirmationRepository channelConfirmationRepository)
    {
        _registerCodeConfirmationOutput = registerCodeConfirmationOutput;
        _channelConfirmationRepository = channelConfirmationRepository;
        _referentialService = referentialService;
        _userRepository = userRepository;
        _userFactory = userFactory;
    }
    
    public async Task<Unit> Handle(RegisterCodeConfirmationCommand request, CancellationToken cancellationToken)
    {
        Partner partner = await _referentialService.GetPartnerByCodeAsync(request.PartnerCode, true);

        var user = await _userFactory.BuildByPartnerAndExternalUserIdAsync(partner, request.ExternalUserId);
        
        if (user is null)
        {
            _registerCodeConfirmationOutput.NotFound($"no user associated to '{request.ExternalUserId}' exists.");
            return Unit.Value;
        }

        user.AddChannelCodeConfirmation(request.ChannelType);
        await _userRepository.SaveAsync(user).ConfigureAwait(false);

        IReadOnlyCollection<ChannelConfirmation> channelCodes = user.GetChannel(request.ChannelType).ChannelCodes;
        await _channelConfirmationRepository
            .UpsertAsync(user.UserIdentifier.UserId, request.ChannelType, channelCodes).ConfigureAwait(false);

        RegisterCodeConfirmationDto registerCode = GetChannel(request.ChannelType, user);
        _registerCodeConfirmationOutput.Ok(registerCode);
        return Unit.Value;
    }

    #region helpers

    private RegisterCodeConfirmationDto GetChannel(ChannelType channelType, User user)
    {
        Channel channel = user.GetChannel(channelType);
        return new RegisterCodeConfirmationDto(channel.ChannelType.Label,
            channel.ChannelValue,
            channel.ChannelCodes.OrderByDescending(x => x.ExpirationDate).First().CodeConfirmation);
    }

    #endregion
}