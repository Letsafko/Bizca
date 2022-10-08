namespace Bizca.Bff.Application.UseCases.UpdateSubscription
{
    using Core.Application.Commands;
    using Domain.Entities.Subscription;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using Domain.Referentials.Procedure;
    using Domain.Referentials.Procedure.Exceptions;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateSubscriptionUseCase : ICommandHandler<UpdateSubscriptionCommand>
    {
        private readonly IProcedureRepository procedureRepository;
        private readonly IUpdateSubscriptionOutput subscriptionOutput;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUserRepository userRepository;

        public UpdateSubscriptionUseCase(ISubscriptionRepository subscriptionRepository,
            IUpdateSubscriptionOutput subscriptionOutput,
            IProcedureRepository procedureRepository,
            IUserRepository userRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.procedureRepository = procedureRepository;
            this.subscriptionOutput = subscriptionOutput;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(command.ExternalUserId);
            if (user is null) throw new UserDoesNotExistException($"user {command.ExternalUserId} does not exist.");

            Procedure procedure = await GetProcedureAsync(command);
            user.UpdateSubscription(command.SubscriptionCode, procedure);

            await subscriptionRepository.UpsertAsync(user.UserIdentifier.UserId, user.Subscriptions);
            subscriptionOutput.Ok(user.GetSubscriptionByCode(command.SubscriptionCode));
            return Unit.Value;
        }

        private async Task<Procedure> GetProcedureAsync(UpdateSubscriptionCommand command)
        {
            return await procedureRepository.GetProcedureByTypeIdAndCodeInseeAsync(int.Parse(command.ProcedureTypeId),
                       command.CodeInsee)
                   ?? throw new ProcedureDoesNotExistException(
                       $"procedureType::{command.ProcedureTypeId} with codeInsee::{command.CodeInsee} does not exist.");
        }
    }
}