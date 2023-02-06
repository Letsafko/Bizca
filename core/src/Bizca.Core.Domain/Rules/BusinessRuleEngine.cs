namespace Bizca.Core.Domain.Rules
{
    using Configuration;
    using Exception;
    using Exceptions;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class BusinessRuleEngine<TRequest>
    {
        private readonly IEnumerable<IBusinessRule<TRequest>> _businessRules;
        private readonly RuleOptions _ruleOptions;
        protected BusinessRuleEngine(IEnumerable<IBusinessRule<TRequest>> businessRules,
            IOptions<RuleOptions> ruleOptions)
        {
            _ruleOptions = ruleOptions.Value;
            _businessRules = businessRules;
        }
        
        public async Task CheckRulesAsync(TRequest request)
        {
            var checkResults = new List<CheckResult>();
            foreach (var rule in _businessRules)
            {
                var checkResult = await rule.CheckAsync(request);
                if (checkResult.Success || !_ruleOptions.AbortOnError) 
                    continue;
                
                checkResults.Add(checkResult);
                break;
            }

            ManageCheckResults(checkResults);
        }
        
        private static void ManageCheckResults(ICollection<CheckResult> checkResults)
        {
            if (!checkResults.Any()) 
                return;
        
            var domainFailures = checkResults
                .Select(x => new DomainFailure(x.CheckReport.ErrorMessage, 
                    x.CheckReport.PropertyName));
            
            throw new RuleCheckException("Check rules failed", domainFailures: domainFailures);
        }
    }
}