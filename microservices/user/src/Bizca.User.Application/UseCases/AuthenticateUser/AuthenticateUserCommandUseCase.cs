﻿namespace Bizca.User.Application.UseCases.AuthenticateUser
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

    public sealed class AuthenticateUserCommandUseCase : ICommandHandler<AuthenticateUserCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IPasswordHasher passwordHasher;
        private readonly IReferentialService referentialService;
        private readonly IAuthenticateUserOutput authenticateUserOutput;
        public AuthenticateUserCommandUseCase(IUserFactory userFactory,
            IPasswordHasher passwordHasher,
            IReferentialService referentialService,
            IAuthenticateUserOutput authenticateUserOutput)
        {
            this.userFactory = userFactory;
            this.passwordHasher = passwordHasher;
            this.referentialService = referentialService;
            this.authenticateUserOutput = authenticateUserOutput;
        }

        private const string message = "username and/or password do not match.";
        public async Task<Unit> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            Partner partner = await referentialService.GetPartnerByCodeAsync(request.PartnerCode, true).ConfigureAwait(false);
            IUser response = await userFactory.BuildByPartnerAndChannelResourceAsync(partner, request.ResourceLogin).ConfigureAwait(false);
            if (response is UserNull)
            {
                authenticateUserOutput.Invalid(message);
                return Unit.Value;
            }

            var user = response as User;
            if (!user.Passwords.Any(x => x.Active))
            {
                authenticateUserOutput.Invalid(message);
                return Unit.Value;
            }

            if (!user.Profile.Channels.Any(x => x.Confirmed && x.ChannelValue.Equals(request.ResourceLogin, StringComparison.OrdinalIgnoreCase)))
            {
                authenticateUserOutput.Invalid(message);
                return Unit.Value;
            }

            Password password = user.Passwords.Single(x => x.Active);
            bool authenticated = passwordHasher.VerifyHashedPassword(request.Password,
                    Convert.FromBase64String(password.PasswordHash),
                    Convert.FromBase64String(password.SecurityStamp));

            if (authenticated)
            {
                authenticateUserOutput.Ok(user);
                return Unit.Value;
            }

            authenticateUserOutput.Invalid(message);
            return Unit.Value;
        }
    }
}