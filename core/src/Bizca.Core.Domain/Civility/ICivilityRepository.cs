namespace Bizca.Core.Domain.Civility
{
    using System.Threading.Tasks;

    public interface ICivilityRepository
    {
        Task<Civility> GetByIdAsync(int civilityId);
    }
}