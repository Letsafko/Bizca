namespace Bizca.User.Domain.Agregates.Users.Factories
{
    using System.Threading.Tasks;

    public interface IUserFactory
    {
        Task<IUser> CreateAsync(UserRequest request);
    }
}