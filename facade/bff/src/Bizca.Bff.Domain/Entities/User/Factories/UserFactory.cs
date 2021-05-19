namespace Bizca.Bff.Domain.Entities.User.Factories
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public sealed class UserFactory : IUserFactory
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUserRepository userRepository;
        public UserFactory(IUserRepository userRepository,
            ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.userRepository = userRepository;

        }

        private const string partnerCode = "Bizca";
        public async Task<User> BuildAsync(string externalUserId)
        {
            (dynamic user, IEnumerable<dynamic> subscriptions) = await GetEntitiesAsync(externalUserId);
            if(user is null)
            {
                throw new UserDoesNotExistException($"user {externalUserId} does not exist.");
            }

            var subscriptionsAlreadyCreated = new List<Subscription>();
            if(subscriptions?.Any() == true)
            {
                foreach(dynamic subscription in subscriptions)
                {
                    Subscription subscriptionToAdd = GetSubscription(subscription);
                    subscriptionsAlreadyCreated.Add(subscriptionToAdd);
                }
            }

            UserProfile userProfile = GetUserProfile(user);
            return new User(new UserIdentifier(externalUserId, partnerCode), 
                userProfile, 
                subscriptionsAlreadyCreated);
        }
        public User Create(UserRequest request)
        {
            UserIdentifier userIdentifier = GetUserIdentifier(request);
            UserProfile userProfile = GetUserProfile(request);
            return new User
            (
                userIdentifier, 
                userProfile
            );
        }

        #region private helpers

        private async Task<(dynamic user, IEnumerable<dynamic> subscriptions)> GetEntitiesAsync(string externalUserId)
        {
            Task<IEnumerable<dynamic>> subscriptionsTask = subscriptionRepository.GetSubscriptionsAsync(externalUserId);
            Task<dynamic> userTask = userRepository.GetAsync(externalUserId);
            await Task.WhenAll(subscriptionsTask, userTask);
            return
            (
                userTask.Result,
                subscriptionsTask.Result
            );
        }
        private UserIdentifier GetUserIdentifier(UserRequest request)
        {
            return new UserIdentifier(request.ExternalUserId, request.PartnerCode);
        }
        private Subscription GetSubscription(dynamic subscription)
        {
            return null;
        }
        private UserProfile GetUserProfile(UserRequest request)
        {
            return new UserProfile(request.Civility,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email);
        }
        
        private UserProfile GetUserProfile(dynamic user)
        {
            var userProfile = new UserProfile((Civility)user.civility,
                user.firstName,
                user.lastName,
                user.phoneNumber,
                user.whatsapp,
                user.email);

            userProfile.SetChannelConfirmationStatus((ChannelConfirmationStatus)user.ChannelConfirmationStatus);
            userProfile.SetChannelActivationStatus((ChannelActivationStatus)user.ChannelActivationStatus);
            return userProfile;
        }

        #endregion
    }
}
