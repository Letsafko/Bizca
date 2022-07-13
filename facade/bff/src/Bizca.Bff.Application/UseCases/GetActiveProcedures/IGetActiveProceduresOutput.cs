namespace Bizca.Bff.Application.UseCases.GetActiveProcedures
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using System.Collections.Generic;
    public interface IGetActiveProceduresOutput
    {
        void Ok(IEnumerable<Procedure> procedures);
    }
}
