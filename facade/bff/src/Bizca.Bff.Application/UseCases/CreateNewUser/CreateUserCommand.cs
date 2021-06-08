﻿namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Core.Application.Commands;

    public sealed class CreateUserCommand : ICommand
    {
        public CreateUserCommand(string externalUserId,
            string partnerCode,
            string civility,
            string phoneNumber,
            string firstName,
            string lastName,
            string whatsapp,
            string email)
        {
            ExternalUserId = externalUserId;
            PartnerCode = partnerCode;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            Civility = civility;
            LastName = lastName;
            Whatsapp = whatsapp;
            Email = email;
        }

        public string EconomicActivity { get; }
        public string ExternalUserId { get; }
        public string PartnerCode { get; }
        public string PhoneNumber { get; }
        public string Civility { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Whatsapp { get; }
        public string Email { get; }
    }
}
