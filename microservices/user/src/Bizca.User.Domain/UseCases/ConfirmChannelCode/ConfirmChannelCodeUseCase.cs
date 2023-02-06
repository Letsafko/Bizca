namespace Bizca.User.Application.UseCases.ConfirmChannelCode;

using Core.Domain.Cqrs.Commands;
using Core.Domain.Referential.Model;
using Core.Domain.Referential.Services;
using Domain.Agregates;
using Domain.Agregates.Factories;
using Domain.Agregates.Repositories;
using Domain.Entities.Channel;
using Domain.Entities.Channel.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public sealed class ConfirmChannelCodeUseCase : ICommandHandler<ChannelConfirmationCommand>
{
    private readonly IChannelRepository channelRepository;
    private readonly IConfirmChannelCodeOutput output;
    private readonly IReferentialService referentialService;
    private readonly IUserFactory userFactory;
    private readonly IUserRepository userRepository;

    public ConfirmChannelCodeUseCase(IUserFactory userFactory,
        IUserRepository userRepository,
        IConfirmChannelCodeOutput output,
        IChannelRepository channelRepository,
        IReferentialService referentialService)
    {
        this.output = output;
        this.userFactory = userFactory;
        this.userRepository = userRepository;
        this.channelRepository = channelRepository;
        this.referentialService = referentialService;
    }

    /// <summary>
    ///     Handles confirmation of a channel.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task<Unit> Handle(ChannelConfirmationCommand request, CancellationToken cancellationToken)
    {
        var partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true);
        var user = await userFactory.BuildByPartnerAndExternalUserIdAsync(partner, request.ExternalUserId);
        if (user is null)
        {
            output.NotFound($"no user associated to '{request.ExternalUserId}' exists.");
            return Unit.Value;
        }
        
        user.ApplyConfirmationCode(request.ChannelType, request.CodeConfirmation);
        var channel = user.GetChannel(request.ChannelType);
        
        await userRepository.SaveAsync(user);
        await channelRepository.SaveAsync(user.UserIdentifier.UserId, user.Profile.Channels)
            .ConfigureAwait(false);

        output.Ok(new ConfirmChannelCodeDto(channel.ChannelType, channel.ChannelValue, confirmed));
        return Unit.Value;
    }
}