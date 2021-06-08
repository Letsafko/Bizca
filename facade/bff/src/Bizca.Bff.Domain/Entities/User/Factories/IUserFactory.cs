namespace Bizca.Bff.Domain.Entities.User.Factories
{
    using System.Threading.Tasks;
    public interface IUserFactory
    {
        Task<User> BuildAsync(string externalUserId);
        User Create(UserRequest request);
    }
}