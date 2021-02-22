namespace Bizca.User.Application.UseCases.ActivateUser
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IActivateUserOutput output; 
        private readonly IReferentialService referentialService;
        public ActivateUserCommandHandler(IUserFactory userFactory, IReferentialService referentialService, IActivateUserOutput output)
        {
            this.output = output;
            this.userFactory = userFactory;
            this.referentialService = referentialService;
        }

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if(response is UserNull)
            {
                output.NotFound($"user::{request.ExternalUserId} does not exist.");
                return Unit.Value;
            }

            var user = response as User;
            if (!user.Channels.Any(x => x.Confirmed))
            {
                output.Invalid($"user::{request.ExternalUserId} does not have any confirmed channel.");
                return Unit.Value;
            }

            return Unit.Value;
        }
    }
}