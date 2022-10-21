namespace Bizca.Bff.Application.UseCases.GetProcedures
{
    using Domain.Referential.Procedure;
    using Domain.Referential.Procedure.ValueObjects;
    using System.Collections.Generic;

    public interface IGetProceduresOutput
    {
        void Ok(Dictionary<Organism, IEnumerable<Procedure>> procedures);
    }
}