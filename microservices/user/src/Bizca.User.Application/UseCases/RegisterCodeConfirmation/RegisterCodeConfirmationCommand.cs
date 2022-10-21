﻿namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Core.Domain.Cqrs.Commands;
    using Domain;

    public sealed class RegisterCodeConfirmationCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public ChannelType ChannelType { get; set; }
    }
}