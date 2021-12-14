namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.User.Domain;

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
            command.ChannelType = ChannelType.GetByLabel(channel);
            return this;
        }

        /// <summary>
        ///     Sets partner code identifier.
        /// </summary>
        /// <param name="partnerCode"></param>
        /// <returns></returns>
        public RegisterCodeConfirmationBuilder WithPartnerCode(string partnerCode)
        {
            command.PartnerCode = partnerCode;
            return this;
        }

        /// <summary>
        ///     Sets user identifier of partner.
        /// </summary>
        /// <param name="externalUserId"></param>
        /// <returns></returns>
        public RegisterCodeConfirmationBuilder WithExternalUserId(string externalUserId)
        {
            command.ExternalUserId = externalUserId;
            return this;
        }
    }
}