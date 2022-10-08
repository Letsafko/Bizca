namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Domain;

    public sealed class ConfirmChannelCodeCommandBuilder
    {
        private readonly ChannelConfirmationCommand command;

        private ConfirmChannelCodeCommandBuilder()
        {
            command = new ChannelConfirmationCommand();
        }

        /// <summary>
        ///     Gets instance of <see cref="ConfirmChannelCodeCommandBuilder" />
        /// </summary>
        public static ConfirmChannelCodeCommandBuilder Instance => new ConfirmChannelCodeCommandBuilder();

        /// <summary>
        ///     Build code confirmation command.
        /// </summary>
        public ChannelConfirmationCommand Build()
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
            command.ChannelType = ChannelType.GetByLabel(channel);
            return this;
        }

        /// <summary>
        ///     Sets partner code identifier.
        /// </summary>
        /// <param name="partnerCode"></param>
        /// <returns></returns>
        public ConfirmChannelCodeCommandBuilder WithPartnerCode(string partnerCode)
        {
            command.PartnerCode = partnerCode;
            return this;
        }

        /// <summary>
        ///     Sets user identifier of partner.
        /// </summary>
        /// <param name="externalUserId"></param>
        /// <returns></returns>
        public ConfirmChannelCodeCommandBuilder WithExternalUserId(string externalUserId)
        {
            command.ExternalUserId = externalUserId;
            return this;
        }

        /// <summary>
        ///     Sets code confirmation.
        /// </summary>
        /// <param name="confirmationCode"></param>
        public ConfirmChannelCodeCommandBuilder WithConfirmationCode(string confirmationCode)
        {
            command.CodeConfirmation = confirmationCode;
            return this;
        }
    }
}