﻿namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserBirthCityMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes =
                (MandatoryUserFlags.BirthCity & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 ||
                !string.IsNullOrWhiteSpace(request.BirthCity);
            if (!succes)
                failure = new DomainFailure("birth city is mandatory.",
                    nameof(request.BirthCity),
                    typeof(BirthCityIsMandatoryException));
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}