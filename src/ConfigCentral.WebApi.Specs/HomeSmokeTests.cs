using System.Net;
using FluentAssertions;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace ConfigCentral.WebApi.Specs
{
    public class HomeSmokeTests
    {
        [Test]
        public void ShouldReturnHttp200Ok()
        {
            using (var server = TestServer.Create<WebPipeline>())
            {
                var response = server.HttpClient.GetAsync("/").Result;

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}