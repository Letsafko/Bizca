namespace Bizca.Bff.Application.UseCases.GetActiveProcedures
{
    using Domain.Referentials.Procedure;
    using System.Collections.Generic;

    public interface IGetActiveProceduresOutput
    {
        void Ok(IEnumerable<Procedure> procedures);
    }
}