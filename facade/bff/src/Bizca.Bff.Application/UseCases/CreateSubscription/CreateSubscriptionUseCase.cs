namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Factories;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateSubscriptionUseCase : ICommandHandler<CreateSubscriptionCommand>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IEventService eventService;
        private readonly IUserFactory userFactory;
        public CreateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            IEventService eventService,
            IUserFactory userFactory)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.eventService = eventService;
            this.userFactory = userFactory;
        }

        public async Task<Unit> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            User user = await userFactory.BuildAsync(request.ExternalUserId);
            return Unit.Value;
        }
    }
}
