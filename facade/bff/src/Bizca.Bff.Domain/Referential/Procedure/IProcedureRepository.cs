namespace Bizca.Bff.Domain.Referential.Procedure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProcedureRepository
    {
        Task<Procedure> GetProcedureByTypeIdAndCodeInseeAsync(int procedureTypeId, string codeInsee);
        Task<IEnumerable<Procedure>> GetProceduresByActiveSubscriptionsAsync();
        Task<IEnumerable<Procedure>> GetProceduresAsync();
    }
}