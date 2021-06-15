namespace Bizca.Bff.Application.UseCases.CreateSubscription
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Factories;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateSubscriptionUseCase : ICommandHandler<CreateSubscriptionCommand>
    {
        private readonly ICreateSubscriptionOutput createSubscriptionOutput;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ISubscriptionFactory subscriptionFactory;
        private readonly IUserRepository userRepository;
        public CreateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            ICreateSubscriptionOutput createSubscriptionOutput,
            ISubscriptionFactory subscriptionFactory,
            IUserRepository userRepository)
        {
            this.createSubscriptionOutput = createSubscriptionOutput;
            this.subscriptionRepository = subscriptionRepository;
            this.subscriptionFactory = subscriptionFactory;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetAsync(command.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {command.ExternalUserId} does not exist.");
            }

            Subscription subscription = await GetSubscriptionAsync(command);
            user.AddSubscription(subscription);
            await subscriptionRepository.UpsertAsync(user.UserIdentifier.UserId, user.Subscriptions);
            createSubscriptionOutput.Ok(subscription);
            return Unit.Value;
        }

        #region private helpers

        private async Task<Subscription> GetSubscriptionAsync(CreateSubscriptionCommand command)
        {
            SubscriptionRequest request = GetSubscriptionRequest(command);
            return await subscriptionFactory.CreateAsync(request);
        }
        private SubscriptionRequest GetSubscriptionRequest(CreateSubscriptionCommand command)
        {
            return new SubscriptionRequest(command.ExternalUserId,
                command.CodeInsee,
                int.Parse(command.ProcedureTypeId),
                string.IsNullOrWhiteSpace(command.BundleId) ? default(int?) : int.Parse(command.BundleId),
                command.FirstName,
                command.LastName,
                command.PhoneNumber,
                command.Whatsapp,
                command.Email);
        }

        #endregion

    }
}
