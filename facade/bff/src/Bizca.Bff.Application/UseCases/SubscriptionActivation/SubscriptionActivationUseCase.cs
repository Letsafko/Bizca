namespace Bizca.Bff.Application.UseCases.SubscriptionActivation
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SubscriptionActivationUseCase : ICommandHandler<SubscriptionActivationCommand>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ISubscriptionActivationOutput subscriptionOutput;
        private readonly IUserRepository userRepository;
        public SubscriptionActivationUseCase(ISubscriptionRepository subscriptionRepository,
            ISubscriptionActivationOutput subscriptionOutput,
            IUserRepository userRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.subscriptionOutput = subscriptionOutput;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(SubscriptionActivationCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetAsync(command.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {command.ExternalUserId} does not exist.");
            }

            Subscription subscription;
            var isFreeze = bool.Parse(command.Freeze);
            if (!isFreeze) 
            {
                subscription = user.ActivateSubscription(command.SubscriptionCode);
            }
            else
            {
                subscription = user.DesactivateSubscription(command.SubscriptionCode);
            }

            await subscriptionRepository.UpsertAsync(user.UserIdentifier.UserId, user.Subscriptions);
            subscriptionOutput.Ok(subscription);
            return Unit.Value;
        }
    }
}
