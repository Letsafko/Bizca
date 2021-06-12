namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Factories;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Factories;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateSubscriptionUseCase : ICommandHandler<UpdateSubscriptionCommand>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ISubscriptionFactory subscriptionFactory;
        private readonly IUserFactory userFactory;
        public UpdateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            ISubscriptionFactory subscriptionFactory,
            IUserFactory userFactory)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.subscriptionFactory = subscriptionFactory;
            this.userFactory = userFactory;
        }

        public async Task<Unit> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            User user = await userFactory.BuildAsync(command.ExternalUserId);
            //user.AddSubscription(subscription);
            //await subscriptionRepository.UpsertAsync(user.Id, user.Subscriptions);
            return Unit.Value;
        }

        #region private helpers

        #endregion

    }
}
