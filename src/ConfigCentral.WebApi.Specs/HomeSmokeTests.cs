using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigCentral.WebApi.Specs
{
    public class HomeSmokeTests : ApiAcceptanceTestBase
    {
        [Test]
        public void ShouldReturnHttp200Ok()
        {
            var response = Server.HttpClient.GetAsync("/")
                .Result;

            response.StatusCode.Should()
                .Be(HttpStatusCode.OK);
        }
    }
}