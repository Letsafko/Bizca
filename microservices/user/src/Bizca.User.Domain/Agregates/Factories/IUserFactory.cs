namespace Bizca.User.Domain.Agregates.Factories
{
    using Bizca.Core.Domain.Partner;
    using System.Threading.Tasks;

    public interface IUserFactory
    {
        Task<IUser> UpdateAsync(UserRequest request);
        Task<IUser> CreateAsync(UserRequest request);
        Task<IUser> BuildAsync(Partner partner, string externalUserId);
    }
}