namespace Bizca.Bff.Domain.Referentials.Procedure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IProcedureRepository
    {
        Task<IEnumerable<Procedure>> GetProceduresAsync();
        Task<Procedure> GetProcedureByIdAsync(int id);
    }
}