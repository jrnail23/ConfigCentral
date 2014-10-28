using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace ConfigCentral.WebApi.Specs
{
    public abstract class ApiAcceptanceTestBase
    {
        [SetUp]
        public void SetUpTestServer()
        {
            Server = TestServer.Create<WebPipeline>();
        }

        [TearDown]
        public void TearDownTestServer()
        {
            Server.Dispose();
            Server = null;
        }

        protected TestServer Server;
    }
}