namespace Bizca.Bff.Application.UseCases.GetProcedures
{
    using Domain.Referentials.Procedure;
    using Domain.Referentials.Procedure.ValueObjects;
    using System.Collections.Generic;

    public interface IGetProceduresOutput
    {
        void Ok(Dictionary<Organism, IEnumerable<Procedure>> procedures);
    }
}