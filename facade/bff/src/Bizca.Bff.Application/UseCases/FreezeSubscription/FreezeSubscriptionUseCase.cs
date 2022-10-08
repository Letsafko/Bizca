namespace Bizca.Bff.Application.UseCases.FreezeSubscription
{
    using Core.Application.Commands;
    using Domain.Entities.Subscription;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class FreezeSubscriptionUseCase : ICommandHandler<FreezeSubscriptionCommand>
    {
        private readonly IFreezeSubscriptionOutput subscriptionOutput;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUserRepository userRepository;

        public FreezeSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            IFreezeSubscriptionOutput subscriptionOutput,
            IUserRepository userRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.subscriptionOutput = subscriptionOutput;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(FreezeSubscriptionCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(command.ExternalUserId);
            if (user is null) throw new UserDoesNotExistException($"user {command.ExternalUserId} does not exist.");

            Subscription subscription;
            bool isFreeze = bool.Parse(command.Freeze);
            if (!isFreeze)
                subscription = user.UnFreezeSubscription(command.SubscriptionCode);
            else
                subscription = user.FreezeSubscription(command.SubscriptionCode);

            await subscriptionRepository.UpsertAsync(user.UserIdentifier.UserId, user.Subscriptions);
            subscriptionOutput.Ok(subscription);
            return Unit.Value;
        }
    }
}