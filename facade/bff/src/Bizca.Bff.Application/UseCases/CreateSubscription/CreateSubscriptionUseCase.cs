namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Factories;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Factories;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateSubscriptionUseCase : ICommandHandler<CreateSubscriptionCommand>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ISubscriptionFactory subscriptionFactory;
        private readonly IUserFactory userFactory;
        public CreateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            ISubscriptionFactory subscriptionFactory,
            IUserFactory userFactory)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.subscriptionFactory = subscriptionFactory;
            this.userFactory = userFactory;
        }

        public async Task<Unit> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            (Subscription subscription, User user) = await GetEntities(command);
            user.AddSubscription(subscription);
            await subscriptionRepository.UpsertAsync(user.Id, user.Subscriptions);
            return Unit.Value;
        }

        #region private helpers

        private async Task<(Subscription subscription, User user)> GetEntities(CreateSubscriptionCommand command)
        {
            SubscriptionRequest request = GetSubscriptionRequest(command);
            Task<Subscription> subscriptionTask = subscriptionFactory.CreateAsync(request);
            Task<User> userTask = userFactory.BuildAsync(request.ExternalUserId);
            await Task.WhenAll(userTask, subscriptionTask);
            return
            (
                subscriptionTask.Result,
                userTask.Result
            );
        }
        private SubscriptionRequest GetSubscriptionRequest(CreateSubscriptionCommand command)
        {
            return new SubscriptionRequest(command.ExternalUserId,
                command.CodeInsee,
                command.ProcedureTypeId,
                command.BundleId,
                command.FirstName,
                command.LastName,
                command.PhoneNumber,
                command.Whatsapp,
                command.Email);
        }

        #endregion

    }
}
