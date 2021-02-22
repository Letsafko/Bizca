namespace Bizca.User.Application.UseCases.RegisterPassword
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RegisterPasswordCommandHandler : ICommandHandler<RegisterPasswordCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IPasswordHasher passwordHasher;
        private readonly IPasswordRepository passwordRepository;
        private readonly IReferentialService referentialService;
        private readonly IRegisterPasswordOutput registerPasswordOutput;
        public RegisterPasswordCommandHandler(IUserFactory userFactory,
            IPasswordRepository passwordRepository,
            IPasswordHasher passwordHasher,
            IReferentialService referentialService,
            IRegisterPasswordOutput registerPasswordOutput)
        {
            this.userFactory = userFactory;
            this.passwordHasher = passwordHasher;
            this.passwordRepository = passwordRepository;
            this.referentialService = referentialService;
            this.registerPasswordOutput = registerPasswordOutput;
        }

        public async Task<Unit> Handle(RegisterPasswordCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if(response is UserNull)
            {
                registerPasswordOutput.NotFound($"user::{request.ExternalUserId} does not exist.");
                return Unit.Value;
            }

            (string passwordHash, string securityStamp) = passwordHasher.CreateHashPassword(request.Password);
            response.AddNewPasword(passwordHash, securityStamp);

            var user = response as User;
            await passwordRepository.AddAsync(user.Id, user.Passwords.ToList()).ConfigureAwait(false);

            registerPasswordOutput.Ok(new RegisterPasswordDto(true));
            return Unit.Value;
        }
    }
}
