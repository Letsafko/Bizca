namespace Bizca.Bff.Application.UseCases.GetProceduresForActiveSubscriptions
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using System.Collections.Generic;
    public interface IGetProceduresForActiveSubscriptionsOutput
    {
        void Ok(IEnumerable<Procedure> procedures);
    }
}
