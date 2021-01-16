namespace Bizca.User.Domain.Agregates.Users.Factories
{
    public interface IUserChannelConfirmationFactory
    {
        ChannelCodeConfirmation Create(NotificationChanels requestedChannel, User user);
    }
}
