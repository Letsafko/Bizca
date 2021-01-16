namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Bizca.User.Domain.Agregates.Users;
    using System;

    public sealed class ConfirmChannelCodeCommandBuilder
    {
        private readonly ConfirmChannelCodeCommand command;
        private ConfirmChannelCodeCommandBuilder()
        {
            command = new ConfirmChannelCodeCommand();
        }

        /// <summary>
        ///     Gets instance of <see cref="ConfirmChannelCodeCommandBuilder"/>
        /// </summary>
        public static ConfirmChannelCodeCommandBuilder Instance => new ConfirmChannelCodeCommandBuilder();

        /// <summary>
        ///     Build code confirmation command.
        /// </summary>
        public ConfirmChannelCodeCommand Build()
        {
            return command;
        }

        /// <summary>
        ///     Sets channel type.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public ConfirmChannelCodeCommandBuilder WithChannel(string channel)
        {
            if (string.IsNullOrWhiteSpace(channel))
            {
                command.ModelState.Add(nameof(channel), $"{nameof(channel)} is required.");
            }
            else if (Enum.TryParse(channel, true, out NotificationChanels channelId))
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
        public ConfirmChannelCodeCommandBuilder WithPartnerCode(string partnerCode)
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
        public ConfirmChannelCodeCommandBuilder WithExternalUserId(string externalUserId)
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

        /// <summary>
        ///     Sets code confirmation.
        /// </summary>
        /// <param name="confirmationCode"></param>
        public ConfirmChannelCodeCommandBuilder WithConfirmationCode(string confirmationCode)
        {
            if (string.IsNullOrWhiteSpace(confirmationCode))
            {
                command.ModelState.Add(nameof(confirmationCode), $"{nameof(confirmationCode)} is required.");
            }
            else
            {
                command.CodeConfirmation = confirmationCode;
            }
            return this;
        }
    }
}
