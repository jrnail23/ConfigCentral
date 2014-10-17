using Microsoft.Owin.Testing;
using NUnit.Framework;
using TestStack.BDDfy;

namespace ConfigCentral.AcceptanceTests
{
    [TestFixture]
    [Story(AsA = "As an application developer", IWant = "I want to add a new application to ConfigCentral",
        SoThat = "So that I can begin managing its configuration data")]
    public class AddNewApplication
    {
        [SetUp]
        public void SetUpTestServer()
        {
            _server = TestServer.Create<WebPipeline>();
        }

        [TearDown]
        public void TearDownTestServer()
        {
            _server.Dispose();
            _server = null;
        }

        TestServer _server;

        [Test]
        public void ApplicationHasAlreadyBeenAdded() {}

        [Test]
        public void NewApplication() {}
    }
}