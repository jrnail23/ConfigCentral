using System;
using System.Net;
using NUnit.Framework;
using TestStack.BDDfy;

namespace ConfigCentral.WebApi.Specs.ApplicationFeature
{
    [TestFixture]
    [Story(AsA = "As an application developer", IWant = "I want to register an application in ConfigCentral",
        SoThat = "So that I can begin managing its configuration data")]
    public class RegisterAnApplication : ApiAcceptanceTestBase
    {
        [SetUp]
        public void SetUpApplicationFixture()
        {
            ApplicationFixture = new ApplicationFixture(Server.HttpClient, RootContainer);
        }

        public ApplicationFixture ApplicationFixture { get; set; }

        [Test]
        public void ApplicationHasAlreadyBeenAdded()
        {
            this.Given(_ => _.ApplicationFixture.MyApplicationHasAlreadyBeenRegistered("MyApplication"))
                .When(
                    _ =>
                        _.ApplicationFixture.IRegisterMyApplicationViaPost(new Uri("/applications", UriKind.Relative),
                            "MyApplication"))
                .Then(_ => _.ApplicationFixture.TheResponseStatusCodeShouldBe(HttpStatusCode.Conflict))
                .And(
                    _ =>
                        _.ApplicationFixture.TheResponseShouldHaveHeader(h => h.Location,
                            new Uri("/applications/MyApplication", UriKind.Relative)))
                .And(_ => _.ApplicationFixture.TheResponseContentShouldBe(new
                {
                    ErrorDescription = string.Format("An application named '{0}' already exists.", "MyApplication")
                }))
                .BDDfy();
        }

        [Test]
        public void NewApplication()
        {
            this.Given(_ => _.ApplicationFixture.MyApplicationIsNotYetRegistered("MyApplication"))
                .When(
                    _ =>
                        _.ApplicationFixture.IRegisterMyApplicationViaPost(new Uri("/applications", UriKind.Relative),
                            "MyApplication"))
                .Then(_ => _.ApplicationFixture.TheResponseStatusCodeShouldBe(HttpStatusCode.Created))
                .And(
                    _ =>
                        _.ApplicationFixture.TheResponseShouldHaveHeader(h => h.Location,
                            new Uri("/applications/MyApplication", UriKind.Relative)))
                .When(_ => _.ApplicationFixture.INavigateToTheResponseLocation())
                .Then(_ => _.ApplicationFixture.TheResponseStatusCodeShouldBe(HttpStatusCode.OK))
                .And(_ => _.ApplicationFixture.TheResponseContentShouldBe(new
                {
                    Name = "MyApplication"
                }))
                .BDDfy();
        }
    }
}