namespace Bizca.Bff.Functional.Test.Steps
{
    using Builders;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;
    using WebApi.UseCases.V10.CreateNewUser;

    [Binding]
    public sealed class UserSteps
    {
        private readonly BffWebApiTestHost host;
        private readonly ScenarioContext scenarioContext;

        public UserSteps(BffWebApiTestHost host, ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.host = host;
        }

        [Given(@"Partner creates randomized information for user '(.*)'.")]
        public void GivenPartnerCreatesRandomizedInformationForUser(string userKey)
        {
            CreateUser request = CreateUserRequestBuilder.Instance.Build();
            scenarioContext.Add(userKey, request);
        }

        [When(@"Partner creates '(.*)' through api.")]
        public async Task WhenPartnerCreatesThroughApi(string userKey)
        {
            //var request = (CreateUser)scenarioContext[userKey];
            //using HttpClient httpClient = host.CreateClient();
            //HttpResponseMessage response = await httpClient.PostAsync("api/v1.0/users", request.GetHttpContent());
            //scenarioContext.Add(nameof(response), response);
        }

        [Then(@"the response should be '(.*)'.")]
        public void ThenTheResponseShouldBe(int expectedResponseCode)
        {
            //scenarioContext.Pending();
        }

        [Then(@"'(.*)' event of type '(.*)' has been published.")]
        public void ThenAnEventOfTypeSendConfirmationEmailNotificationHasBeenPublish(int expectedEventCount
            , string expectedEventName)
        {
            //scenarioContext.Pending();
        }
    }

    [Binding]
    public class ScenarioKook
    {
        [AfterScenario]
        public static void AfterScenario(ScenarioContext scenarioContext)
        {
        }
    }
}