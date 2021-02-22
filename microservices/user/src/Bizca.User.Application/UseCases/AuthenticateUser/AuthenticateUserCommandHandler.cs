namespace Bizca.User.Application.UseCases.AuthenticateUser
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IPasswordHasher passwordHasher;
        private readonly IReferentialService referentialService;
        private readonly IAuthenticateUserOutput authenticateUserOutput;
        public AuthenticateUserCommandHandler(IUserFactory userFactory,
            IPasswordHasher passwordHasher,
            IReferentialService referentialService,
            IAuthenticateUserOutput authenticateUserOutput)
        {
            this.userFactory = userFactory;
            this.passwordHasher = passwordHasher;
            this.referentialService = referentialService;
            this.authenticateUserOutput = authenticateUserOutput;
        }

        public async Task<Unit> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            IUser response = await userFactory.BuildAsync(partner, request.ExternalUserId).ConfigureAwait(false);
            if (response is UserNull)
            {
                authenticateUserOutput.NotFound($"user::{request.ExternalUserId} does not exist.");
                return Unit.Value;
            }

            var user = response as User;
            if (!IsResourceMatchesAnyChannel(request.ResourceLogin, user))
            {
                authenticateUserOutput.Invalid("mismatch username or password.");
                return Unit.Value;
            }

            Password password = user.Passwords.Single(x => x.Active);
            bool authenticated = passwordHasher.VerifyHashedPassword(request.Password,
                                Convert.FromBase64String(password.PasswordHash),
                                Convert.FromBase64String(password.SecurityStamp));

            authenticateUserOutput.Ok(new AuthenticateUserDto(authenticated));
            return Unit.Value;
        }

        private bool IsResourceMatchesAnyChannel(string ressource, User user)
        {
            return user.Passwords.Any(x => x.Active) &&
                   user.Channels.Any(x => x.Confirmed && x.ChannelValue.Equals(ressource, StringComparison.OrdinalIgnoreCase));
        }
    }
}
