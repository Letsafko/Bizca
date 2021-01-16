namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.User.Domain.Agregates.Users;
    using System;

    /// <summary>
    ///     Register code confirmation builder.
    /// </summary>
    public sealed class RegisterCodeConfirmationBuilder
    {
        private readonly RegisterCodeConfirmationCommand command;
        private RegisterCodeConfirmationBuilder()
        {
            command = new RegisterCodeConfirmationCommand();
        }

        /// <summary>
        ///     Gets instance of <see cref="RegisterCodeConfirmationBuilder"/>
        /// </summary>
        public static RegisterCodeConfirmationBuilder Instance => new RegisterCodeConfirmationBuilder();
        
        /// <summary>
        ///     Build register code confirmation command.
        /// </summary>
        /// <returns></returns>
        public RegisterCodeConfirmationCommand Build()
        {
            return command;
        }

        /// <summary>
        ///     Sets channel type.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public RegisterCodeConfirmationBuilder WithChannel(string channel)
        {
            if (string.IsNullOrWhiteSpace(channel))
            {
                command.ModelState.Add(nameof(channel), $"{nameof(channel)} is required.");
            }
            else if(Enum.TryParse(channel, true, out NotificationChanels channelId))
            {
                command.ChannelId = channelId;
            }
            else
            {
                command.ModelState.Add(nameof(channel), $"{nameof(channel)} is invalid.");
            }
            return this;
        }

        /// <summary>
        ///     Sets partner code identifier.
        /// </summary>
        /// <param name="partnerCode"></param>
        /// <returns></returns>
        public RegisterCodeConfirmationBuilder WithPartnerCode(string partnerCode)
        {
            if (string.IsNullOrWhiteSpace(partnerCode))
            {
                command.ModelState.Add(nameof(partnerCode), $"{nameof(partnerCode)} is required.");
            }
            else
            {
                command.PartnerCode = partnerCode;
            }
            return this;
        }


        /// <summary>
        ///     Sets user identifier of partner.
        /// </summary>
        /// <param name="externalUserId"></param>
        /// <returns></returns>
        public RegisterCodeConfirmationBuilder WithExternalUserId(string externalUserId)
        {
            if (string.IsNullOrWhiteSpace(externalUserId))
            {
                command.ModelState.Add(nameof(externalUserId), $"{nameof(externalUserId)} is required.");
            }
            else
            {
                command.ExternalUserId = externalUserId;
            }
            return this;
        }
    }
}
