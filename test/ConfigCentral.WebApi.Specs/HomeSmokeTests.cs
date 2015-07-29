using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigCentral.WebApi.Specs
{
    public class HomeSmokeTests : ApiAcceptanceTestBase
    {
        [Test]
        public async void ShouldReturnHttp200Ok()
        {
            var response = await Server.HttpClient.GetAsync("/");

            response.StatusCode.Should()
                .Be(HttpStatusCode.OK);
        }
    }
}