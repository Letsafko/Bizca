﻿namespace Bizca.Bff.Application.UseCases.GetBundles
{
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetBundlesUseCase : IQueryHandler<GetBundlesQuery>
    {
        private readonly IBundleRepository bundleRepository;
        private readonly IGetBundlesOutput output;
        public GetBundlesUseCase(IBundleRepository bundleRepository,
            IGetBundlesOutput output)
        {
            this.bundleRepository = bundleRepository;
            this.output = output;
        }

        public async Task<Unit> Handle(GetBundlesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Bundle> bundles = await bundleRepository.GetBundlesAsync();
            output.Ok(bundles);
            return Unit.Value;
        }
    }
}