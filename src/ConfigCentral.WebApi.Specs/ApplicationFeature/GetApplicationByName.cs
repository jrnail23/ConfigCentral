using System;
using System.Net;
using ConfigCentral.Application;
using NUnit.Framework;
using TestStack.BDDfy;

namespace ConfigCentral.WebApi.Specs.ApplicationFeature
{
    [TestFixture]
    public class GetApplicationByName : ApiAcceptanceTestBase
    {
        [SetUp]
        public void SetUpApplicationFixture()
        {
            ApplicationFixture = new ApplicationFixture(Server.HttpClient, RootContainer);
        }

        public ApplicationFixture ApplicationFixture { get; set; }

        [Test]
        public void ApplicationDoesNotExist()
        {
            this.Given(_ => _.ApplicationFixture.MyApplicationIsNotYetRegistered("My Great App"))
                .When(_ => _.ApplicationFixture.IGet(new Uri("/applications/My%20Great%20App", UriKind.Relative)))
                .Then(_ => _.ApplicationFixture.TheResponseStatusCodeShouldBe(HttpStatusCode.NotFound))
                .BDDfy();
        }

        [Test]
        public void ApplicationExists()
        {
            this.Given(_ => _.ApplicationFixture.MyApplicationHasAlreadyBeenRegistered("My Great App"))
                .When(_ => _.ApplicationFixture.IGet(new Uri("/applications/My%20Great%20App", UriKind.Relative)))
                .And(_ => _.ApplicationFixture.TheResponseStatusCodeShouldBe(HttpStatusCode.OK))
                .And(_ => _.ApplicationFixture.TheResponseContentShouldBe(new
                {
                    Name = "My Great App"
                }))
                .BDDfy();
        }
    }

    [TestFixture]
    public class ListApplications : ApiAcceptanceTestBase
    {
        [SetUp]
        public void SetUpApplicationFixture()
        {
            ApplicationFixture = new ApplicationFixture(Server.HttpClient, RootContainer);
        }

        public ApplicationFixture ApplicationFixture { get; set; }

        [Test]
        public void ApplicationDoesNotExist()
        {
            this.Given(_ => _.ApplicationFixture.MyApplicationIsNotYetRegistered("My Great App"))
                .When(_ => _.ApplicationFixture.IGet(new Uri("/applications/My%20Great%20App", UriKind.Relative)))
                .Then(_ => _.ApplicationFixture.TheResponseStatusCodeShouldBe(HttpStatusCode.NotFound))
                .BDDfy();
        }

        [Test]
        public void ApplicationsExist()
        {
            this.Given(_ => _.ApplicationFixture.MyApplicationHasAlreadyBeenRegistered("My Great App"))
                .And(_ => _.ApplicationFixture.MyApplicationHasAlreadyBeenRegistered("My Other App"))
                .When(_ => _.ApplicationFixture.IGet(new Uri("/applications", UriKind.Relative)))
                .Then(_ => _.ApplicationFixture.TheResponseStatusCodeShouldBe(HttpStatusCode.OK))
                .And(_ => _.ApplicationFixture.TheResponseContentShouldBe(new[] {new ApplicationState
                {
                    Name = "My Great App"
                },
                    new ApplicationState
                    {
                        Name = "My Other App"
                    }}))
                .BDDfy();
        }
    }
}