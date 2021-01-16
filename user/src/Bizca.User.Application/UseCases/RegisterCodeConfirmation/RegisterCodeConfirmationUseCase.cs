namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Factories;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RegisterCodeConfirmationUseCase : ICommandHandler<RegisterCodeConfirmationCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IPartnerRepository partnerRepository;
        private readonly IRegisterCodeConfirmationOutput output;
        private readonly IUserChannelConfirmationFactory userChannelConfirmationFactory;
        private readonly IUserChannelConfirmationRepository userChannelConfirmationRepository;
        public RegisterCodeConfirmationUseCase(IUserFactory userFactory,
            IPartnerRepository partnerRepository,
            IRegisterCodeConfirmationOutput output,
            IUserChannelConfirmationFactory userChannelConfirmationFactory,
            IUserChannelConfirmationRepository userChannelConfirmationRepository)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.partnerRepository = partnerRepository;
            this.userChannelConfirmationFactory = userChannelConfirmationFactory;
            this.userChannelConfirmationRepository = userChannelConfirmationRepository;
        }

        /// <summary>
        ///     Handles registration of user channel code confirmation.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(RegisterCodeConfirmationCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await partnerRepository.GetByCodeAsync(request.PartnerCode).ConfigureAwait(false);
            if (partner is null)
            {
                request.ModelState.Add(nameof(request.PartnerCode), $"partner::{request.PartnerCode} is invalid.");
            }

            if (!request.ModelState.IsValid)
            {
                output.Invalid(request.ModelState);
                return Unit.Value;
            }

            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if (response is UserNull)
            {
                output.NotFound($"user::{request.ExternalUserId} does not exist.");
                return Unit.Value;
            }

            var user = response as User;
            ChannelCodeConfirmation channelConfirmation = userChannelConfirmationFactory.Create(request.ChannelId, user);
            bool result = await userChannelConfirmationRepository.AddAsync(user.Id, channelConfirmation).ConfigureAwait(false);
            if(result)
            {
                var registerDto = new RegisterCodeConfirmationDto(channelConfirmation.Channel.ToString(),
                    channelConfirmation.ChannelValue,
                    channelConfirmation.CodeConfirmation);

                output.Ok(registerDto);
                return Unit.Value;
            }

            request.ModelState.Add("process", "unable to create code confirmation.");
            output.Invalid(request.ModelState);
            return Unit.Value;
        }
    }
}
