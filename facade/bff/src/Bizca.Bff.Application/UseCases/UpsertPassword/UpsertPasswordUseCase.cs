namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    using Core.Domain;
    using Core.Domain.Cqrs.Commands;
    using Domain.Wrappers.Users;
    using Domain.Wrappers.Users.Requests;
    using Domain.Wrappers.Users.Responses;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpsertPasswordUseCase : ICommandHandler<UpsertPasswordCommand>
    {
        private readonly IUpsertPasswordOutput passwordOutput;
        private readonly IUserPasswordWrapper userPasswordAgent;

        public UpsertPasswordUseCase(IUpsertPasswordOutput passwordOutput,
            IUserWrapper userAgent)
        {
            this.passwordOutput = passwordOutput;
            userPasswordAgent = userAgent;
        }

        public async Task<Unit> Handle(UpsertPasswordCommand command, CancellationToken cancellationToken)
        {
            var userPassword = new UserPasswordRequest(command.PartnerCode,
                command.Password,
                command.ChannelResource);

            IPublicResponse<UserPasswordResponse> response =
                await userPasswordAgent.CreateOrUpdateUserPasswordAsync(userPassword);
            if (!response.Success)
            {
                passwordOutput.Invalid(response);
                return Unit.Value;
            }

            passwordOutput.Ok(new UpsertPasswordDto(response.Data.Success));
            return Unit.Value;
        }
    }
}