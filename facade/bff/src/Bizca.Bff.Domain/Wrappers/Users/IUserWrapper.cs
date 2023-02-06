namespace Bizca.Bff.Domain.Wrappers.Users
{
    public interface IUserWrapper : IUserChannelWrapper,
        IUserProfileWrapper,
        IUserPasswordWrapper,
        IUserAuthenticationWrapper
    {
    }
}