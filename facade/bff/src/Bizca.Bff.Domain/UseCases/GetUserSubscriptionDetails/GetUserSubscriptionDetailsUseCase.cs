﻿namespace Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails
{
    using Core.Domain.Cqrs.Queries;
    using Domain.Entities.Subscription;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUserSubscriptionDetailsUseCase : IQueryHandler<GetUserSubscriptionDetailsQuery>
    {
        private readonly IGetUserSubscriptionDetailsOutput subscriptionDetailsOutput;
        private readonly IUserRepository userRepository;

        public GetUserSubscriptionDetailsUseCase(IGetUserSubscriptionDetailsOutput subscriptionDetailsOutput,
            IUserRepository userRepository)
        {
            this.subscriptionDetailsOutput = subscriptionDetailsOutput;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(GetUserSubscriptionDetailsQuery request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(request.ExternalUserId);
            if (user is null) throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");

            Subscription subscription = user.GetSubscriptionByCode(request.SubscriptionCode, true);
            subscriptionDetailsOutput.Ok(subscription);
            return Unit.Value;
        }
    }
}