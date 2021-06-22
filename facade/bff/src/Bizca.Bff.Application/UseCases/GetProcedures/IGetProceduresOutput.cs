namespace Bizca.Bff.Application.UseCases.GetProcedures
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using System.Collections.Generic;
    public interface IGetProceduresOutput
    {
        void Ok(Dictionary<Organism, IEnumerable<Procedure>> procedures);
    }
}