namespace Bizca.Bff.Application.UseCases.GetProceduresForActiveSubscriptions
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetProceduresForActiveSubscriptionsUseCase : IQueryHandler<GetProceduresForActiveSubscriptionsQuery>
    {
        private readonly IGetProceduresForActiveSubscriptionsOutput output;
        private readonly IProcedureRepository procedureRepository;
        public GetProceduresForActiveSubscriptionsUseCase(IProcedureRepository procedureRepository,
            IGetProceduresForActiveSubscriptionsOutput output)
        {
            this.procedureRepository = procedureRepository;
            this.output = output;
        }

        public async Task<Unit> Handle(GetProceduresForActiveSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Procedure> procedures = await procedureRepository.GetProceduresByActiveSubscriptionsAsync();
            output.Ok(procedures);
            return Unit.Value;
        }
    }
}
