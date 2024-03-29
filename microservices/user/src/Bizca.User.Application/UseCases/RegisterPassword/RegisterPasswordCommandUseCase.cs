﻿namespace Bizca.User.Application.UseCases.RegisterPassword
{
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates;
    using Bizca.User.Domain.Agregates.Factories;
    using Bizca.User.Domain.Agregates.Repositories;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RegisterPasswordCommandUseCase : ICommandHandler<RegisterPasswordCommand>
    {
        private readonly IUserFactory userFactory;
        private readonly IPasswordHasher passwordHasher;
        private readonly IPasswordRepository passwordRepository;
        private readonly IReferentialService referentialService;
        private readonly IRegisterPasswordOutput registerPasswordOutput;
        public RegisterPasswordCommandUseCase(IUserFactory userFactory,
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
            IUser response = await userFactory.BuildByPartnerAndChannelResourceAsync(partner, request.ChannelResource).ConfigureAwait(false);
            if (response is UserNull)
            {
                registerPasswordOutput.NotFound($"no user associated to '{request.ChannelResource}' exists.");
                return Unit.Value;
            }

            var user = response as User;
            if (!user.Profile.Channels.Any(x => x.Confirmed && x.ChannelValue.Equals(request.ChannelResource, StringComparison.OrdinalIgnoreCase)))
            {
                registerPasswordOutput.NotFound($"no active channel associated to '{request.ChannelResource}' exists.");
                return Unit.Value;
            }

            (string passwordHash, string securityStamp) = passwordHasher.CreateHashPassword(request.Password);
            response.AddNewPasword(passwordHash, securityStamp);

            await passwordRepository.AddAsync(user.UserIdentifier.UserId, user.Passwords.ToList()).ConfigureAwait(false);

            registerPasswordOutput.Ok(new RegisterPasswordDto(true));
            return Unit.Value;
        }
    }
}