namespace Bizca.Bff.Domain.Entities.User.Factories
{
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    public sealed class UserFactory : IUserFactory
    {
        public User Create(UserRequest request)
        {
            var userIdentifier = new UserIdentifier(0, request.ExternalUserId);
            var userProfile = new UserProfile(request.Civility,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email,
                ChannelConfirmationStatus.None,
                ChannelActivationStatus.None);

            var user = new User(0, userIdentifier, userProfile);
            user.RegisterNewChannelCodeConfirmation(request.ExternalUserId, ChannelType.Email);
            return user;
        }
    }
}