namespace Bizca.Bff.Application.UseCases.GetProcedures
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetProceduresUseCase : IQueryHandler<GetProceduresQuery>
    {
        private readonly IProcedureRepository procedureRepository;
        private readonly IGetProceduresOutput output;
        public GetProceduresUseCase(IProcedureRepository procedureRepository,
            IGetProceduresOutput output)
        {
            this.procedureRepository = procedureRepository;
            this.output = output;
        }

        public async Task<Unit> Handle(GetProceduresQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Procedure> procedures = await procedureRepository.GetProceduresAsync();
            output.Ok(procedures);
            return Unit.Value;
        }
    }
}
