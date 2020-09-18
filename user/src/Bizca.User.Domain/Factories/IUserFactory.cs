namespace Bizca.User.Domain.Factories
{
    using System.Threading.Tasks;

    public interface IUserFactory
    {
        Task<IUser> CreateAsync(UserRequest request);
    }
}