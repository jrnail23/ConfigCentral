using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigCentral.AcceptanceTests
{
    public class ConfigurationApiTests
    {
        public class RetrievingParameterValueByEnvironment : ApiAcceptanceTestBase
        {
            private HttpResponseMessage Response;

            [SetUp]
            public void InvokeRequest()
            {
                Response = Server.HttpClient
                    .GetAsync("/api/configs/qa/parameter1")
                    .Result;
            }

            [Test]
            public void ShouldReturnHttp200Ok()
            {
                Response.StatusCode.Should()
                    .Be(HttpStatusCode.OK);
            }

            [Test]
            public void ShouldReturnJsonContent()
            {
                Response.Content.Headers.ContentType.MediaType.Should()
                    .Be("application/json");
            }

            [Test]
            public void ShouldReturnExpectedParameterSet()
            {
                var response = Server.HttpClient.GetAsync("/api/configs/qa/parameter1")
                    .Result;

                response.Should()
                    .Match<HttpResponseMessage>(x => x.StatusCode == HttpStatusCode.OK);

                response.Content.ReadAsJsonAsync<string>()
                    .Result.Should()
                    .Be("qa.value1");
            }
        }

        public class RetrievingParameterValueSetByEnvironment : ApiAcceptanceTestBase
        {
            private HttpResponseMessage Response;

            [SetUp]
            public void InvokeRequest()
            {
                Response = Server.HttpClient.GetAsync("/api/configs/qa")
                    .Result;
            }

            [Test]
            public void ShouldReturnHttp200Ok()
            {
                Response.StatusCode.Should()
                    .Be(HttpStatusCode.OK);
            }

            [Test]
            public void ShouldReturnJsonContent()
            {
                Response.Content.Headers.ContentType.MediaType.Should()
                    .Be("application/json");
            }

            [Test]
            public void ShouldReturnExpectedParameterSet()
            {
                Response.Content.ReadAsStringAsync()
                    .Result.Should()
                    .Be("{\"parameter1\":\"qa.value1\",\"parameter2\":\"qa.value2\"}");
            }
        }

        public class UpdatingAParameterValue : ApiAcceptanceTestBase
        {
            private HttpResponseMessage Response;

            [SetUp]
            public void InvokeRequest()
            {
                //var content = new StringContent("qa.value1.updated");
                var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "value", "qa.value1.updated" } });
                Response = Server.HttpClient.PutAsync("/api/configs/qa/parameter1", content)
                    .Result;
            }

            [Test]
            public void ShouldReturnHttp204NoContent()
            {
                Response.StatusCode.Should()
                    .Be(HttpStatusCode.NoContent);
            }

            [Test]
            public void ShouldReturnNoContent()
            {
                Response.Content.ReadAsStringAsync()
                    .Result.Should()
                    .BeEmpty();
            }
        }
    }
}