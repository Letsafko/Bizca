namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class UpsertPasswordUseCase : ICommandHandler<UpsertPasswordCommand>
    {
        private readonly IUpsertPasswordOutput passwordOutput;
        private readonly IUserWrapper userAgent;
        public UpsertPasswordUseCase(IUpsertPasswordOutput passwordOutput,
            IUserWrapper userAgent)
        {
            this.passwordOutput = passwordOutput;
            this.userAgent = userAgent;
        }

        public async Task<Unit> Handle(UpsertPasswordCommand command, CancellationToken cancellationToken)
        {
            var userPassword = new UserPasswordRequest(command.PartnerCode,
                command.Password, 
                command.ChannelResource);

            var ressult = await userAgent.CreateOrUpdateUserPasswordAsync(userPassword);

            passwordOutput.Ok(new UpsertPasswordDto(ressult.Success));
            return Unit.Value;
        }
    }
}
