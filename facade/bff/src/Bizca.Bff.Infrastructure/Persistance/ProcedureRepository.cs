namespace Bizca.Bff.Infrastructure.Persistance
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using Bizca.Core.Domain;
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class ProcedureRepository : IProcedureRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public ProcedureRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getProcedureByTypeIdAndCodeInseeStoredProcedure = "bff.usp_getByIdAndCodeInsee_procedure";
        private const string getProceduresByActiveSubscriptions = "[bff].[usp_getByActiveSubscriptions_procedure]";
        private const string getProceduresStoredProcedure = "bff.usp_getAll_procedure";

        public async Task<IEnumerable<Procedure>> GetProceduresByActiveSubscriptionsAsync()
        {
            IEnumerable<dynamic> results = await unitOfWork.Connection
                .QueryAsync(getProceduresByActiveSubscriptions,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            if (results?.Any() != true)
            {
                return Array.Empty<Procedure>();
            }

            var procedures = new List<Procedure>();
            foreach (dynamic procedure in results)
                procedures.Add(GetProcedure(procedure));

            return procedures;
        }

        public async Task<Procedure> GetProcedureByTypeIdAndCodeInseeAsync(int procedureTypeId, string codeInsee)
        {
            var parameters = new
            {
                procedureTypeId,
                codeInsee
            };

            dynamic result = await unitOfWork.Connection
                .QueryFirstOrDefaultAsync(getProcedureByTypeIdAndCodeInseeStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            return result is null
                ? default(Procedure)
                : GetProcedure(result);
        }

        public async Task<IEnumerable<Procedure>> GetProceduresAsync()
        {
            IEnumerable<dynamic> results = await unitOfWork.Connection
                .QueryAsync(getProceduresStoredProcedure,
                    transaction: unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            if (results?.Any() != true)
            {
                return Array.Empty<Procedure>();
            }

            var procedures = new List<Procedure>();
            foreach (dynamic procedure in results)
                procedures.Add(GetProcedure(procedure));

            return procedures;
        }

        private Procedure GetProcedure(dynamic result)
        {
            var procedureType = new ProcedureType((int)result.procedureTypeId, result.procedureTypeLabel);
            var organism = new Organism((int)result.organismId,
                result.codeInsee,
                result.organismName,
                result.organismHref);
            return new Procedure(procedureType,
                organism,
                result.procedureHref);
        }
    }
}