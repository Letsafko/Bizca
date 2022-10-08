namespace Bizca.Core.Domain.Referential.Repository
{
    using Model;
    using System.Threading.Tasks;

    public interface ICivilityRepository
    {
        Task<Civility> GetByIdAsync(int civilityId);
    }
}