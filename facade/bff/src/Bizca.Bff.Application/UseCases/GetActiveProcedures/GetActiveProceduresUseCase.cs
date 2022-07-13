namespace Bizca.Bff.Application.UseCases.GetActiveProcedures
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetActiveProceduresUseCase : IQueryHandler<GetActiveProceduresQuery>
    {
        private readonly IGetActiveProceduresOutput output;
        private readonly IProcedureRepository procedureRepository;
        public GetActiveProceduresUseCase(IProcedureRepository procedureRepository,
            IGetActiveProceduresOutput output)
        {
            this.procedureRepository = procedureRepository;
            this.output = output;
        }

        public async Task<Unit> Handle(GetActiveProceduresQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Procedure> procedures = await procedureRepository.GetProceduresByActiveSubscriptionsAsync();
            output.Ok(procedures);
            return Unit.Value;
        }
    }
}
