﻿namespace Bizca.Bff.Infrastructure.Persistence
{
    using Bizca.Bff.Domain.ValueObject;
    using Core.Infrastructure.Database;
    using Dapper;
    using Domain.Referential.Bundle;
    using Domain.Referential.Bundle.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class BundleRepository : IBundleRepository
    {
        private const string getBundleByIdStoredProcedure = "bff.usp_getById_bundle";
        private const string getBundlesStoredProcedure = "bff.usp_getAll_bundle";
        private readonly IUnitOfWork unitOfWork;

        public BundleRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Bundle> GetBundleByIdAsync(int bundleId)
        {
            var parameters = new { bundleId };

            dynamic result = await unitOfWork.Connection
                .QueryFirstOrDefaultAsync(getBundleByIdStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            return result is null
                ? default(Bundle)
                : GetBundle(result);
        }

        public async Task<IEnumerable<Bundle>> GetBundlesAsync()
        {
            IEnumerable<dynamic> results = await unitOfWork.Connection
                .QueryAsync(getBundlesStoredProcedure,
                    transaction: unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            if (results?.Any() != true) return Array.Empty<Bundle>();

            var bundles = new List<Bundle>();
            foreach (dynamic bundle in results)
                bundles.Add(GetBundle(bundle));

            return bundles;
        }

        private Bundle GetBundle(dynamic result)
        {
            var identifier = new BundleIdentifier((int)result.bundleId, result.bundleCode, result.bundleLabel);
            Priority priority = Priority.GetByCode((int)result.priority);
            var money = new Money((decimal)result.price, Currency.Euro);
            var settings = new BundleSettings((int)result.intervalInWeeks,
                (int)result.bundleTotalWhatsapp,
                (int)result.bundleTotalEmail,
                (int)result.bundleTotalSms);
            return new Bundle(identifier,
                settings,
                priority,
                money);
        }
    }
}