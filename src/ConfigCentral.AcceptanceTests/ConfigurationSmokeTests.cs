using System.Net;
using FluentAssertions;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace ConfigCentral.AcceptanceTests
{
    public class ConfigurationSmokeTests
    {
        [Test]
        public void ShouldReturnHttp200Ok()
        {
            using (var server = TestServer.Create<WebPipeline>())
            {
                var response = server.HttpClient.GetAsync("/api/configs/myapp/v1.1/test6").Result;

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}