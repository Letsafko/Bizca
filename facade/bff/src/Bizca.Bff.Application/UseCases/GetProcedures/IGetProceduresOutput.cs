namespace Bizca.Bff.Application.UseCases.GetProcedures
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using System.Collections.Generic;
    public interface IGetProceduresOutput
    {
        void Ok(IEnumerable<Procedure> procedures);
    }
}
