namespace Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword
{
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateOrUpdateUserPasswordUseCase : ICommandHandler<CreateOrUpdateUserPasswordCommand>
    {
        private readonly ICreateOrUpdateUserPasswordOutput createOrUpdateUserPasswordOutput;
        private readonly IUserWrapper userAgent;
        public CreateOrUpdateUserPasswordUseCase(IUserWrapper userAgent,
            ICreateOrUpdateUserPasswordOutput createOrUpdateUserPasswordOutput)
        {
            this.createOrUpdateUserPasswordOutput = createOrUpdateUserPasswordOutput;
            this.userAgent = userAgent;
        }

        /// <summary>
        ///     Handle creation or update of user password.
        /// </summary>
        /// <param name="command">Create or update user password command</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(CreateOrUpdateUserPasswordCommand command, CancellationToken cancellationToken)
        {
            var request = new UserPasswordRequest(command.Password, command.Resource);
            UserPasswordResponse response = await userAgent.CreateOrUpdateUserPasswordAsync(request);

            CreateOrUpdateUserPasswordDto createOrUpdateUserPasswordDto = MapTo(response);
            createOrUpdateUserPasswordOutput.Ok(createOrUpdateUserPasswordDto);
            return Unit.Value;
        }

        private CreateOrUpdateUserPasswordDto MapTo(UserPasswordResponse response)
        {
            return new CreateOrUpdateUserPasswordDto(response.Success);
        }
    }
}