namespace Bizca.Bff.Domain.Entities.User.Factories
{
    public interface IUserFactory
    {
        User Create(UserRequest request);
    }
}