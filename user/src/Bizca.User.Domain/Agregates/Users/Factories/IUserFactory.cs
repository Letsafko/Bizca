namespace Bizca.User.Domain.Agregates.Users.Factories
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Partner;
    using System.Threading.Tasks;

    public interface IUserFactory
    {
        Task<IUser> UpdateAsync(UserRequest request);
        Task<Response<IUser>> CreateAsync(UserRequest request);
        Task<IUser> BuildAsync(Partner partner, string externalUserId);
    }
}