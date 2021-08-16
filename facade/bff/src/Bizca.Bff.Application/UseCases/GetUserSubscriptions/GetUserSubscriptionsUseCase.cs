namespace Bizca.Bff.Application.UseCases.GetUserSubscriptions
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUserSubscriptionsUseCase : IQueryHandler<GetUserSubscriptionsQuery>
    {
        private readonly IGetUserSubscriptionsOutput userSubscriptionsOutput;
        private readonly IUserRepository userRepository;
        public GetUserSubscriptionsUseCase(IGetUserSubscriptionsOutput userSubscriptionsOutput,
            IUserRepository userRepository)
        {
            this.userSubscriptionsOutput = userSubscriptionsOutput;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(GetUserSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(request.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");
            }

            userSubscriptionsOutput.Ok(user.Subscriptions);
            return Unit.Value;
        }
    }
}