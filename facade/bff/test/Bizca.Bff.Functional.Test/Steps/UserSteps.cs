namespace Bizca.Bff.Functional.Test.Steps
{
    using Bizca.Bff.Functional.Test.Builders;
    using Bizca.Bff.WebApi.UseCases.V10.CreateNewUser;
    using Bizca.Core.Infrastructure;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class UserSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly BffWebApiTestHost host;
        public UserSteps(BffWebApiTestHost host, ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.host = host;
        }

        [Given(@"Partner creates randomized informations for user '(.*)'.")]
        public void GivenPartnerCreatesRandomizedInformationsForUser(string userKey)
        {
            CreateUser request = CreateUserRequestBuilder.Instance.Build();
            scenarioContext.Add(userKey, request);
        }

        [When(@"Partner creates '(.*)' through api.")]
        public async Task WhenPartnerCreatesThroughApi(string userKey)
        {
            var request = (CreateUser)scenarioContext[userKey];
            using(HttpClient httpClient = host.CreateClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync($"api/v1.0/users", request.GetHttpContent());
                scenarioContext.Add(nameof(response), response);
            }
        }

        [Then(@"the response should be '(.*)'.")]
        public void ThenTheResponseShouldBe(int expectedResponseCode)
        {
            scenarioContext.Pending();
        }

        [Then(@"'(.*)' event of type '(.*)' has been published.")]
        public void ThenAnEventOfTypeSendConfirmationEmalNotificationHasBeenPublish(int expectedEventCount
            , string expectedEventName)
        {
            scenarioContext.Pending();
        }
    }
}