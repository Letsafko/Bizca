namespace Bizca.Bff.Domain.Entities.User.Factories
{
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    public sealed class UserFactory : IUserFactory
    {
        public User Create(UserRequest request)
        {
            const int userId = 0;
            var userIdentifier = new UserIdentifier(userId, request.ExternalUserId);
            var userProfile = new UserProfile(request.Civility,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email,
                ChannelConfirmationStatus.None,
                ChannelActivationStatus.None);

            var user = new User(userId, userIdentifier, userProfile);
            user.RegisterSendConfirmationEmailEvent(request.ExternalUserId, request.Email);
            return user;
        }
    }
}