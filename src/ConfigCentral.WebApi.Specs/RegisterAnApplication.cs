using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ConfigCentral.WebApi.Resources;
using FluentAssertions;
using NUnit.Framework;
using TestStack.BDDfy;

namespace ConfigCentral.WebApi.Specs
{
    [TestFixture]
    [Story(AsA = "As an application developer", IWant = "I want to register an application in ConfigCentral",
        SoThat = "So that I can begin managing its configuration data")]
    public class RegisterAnApplication : ApiAcceptanceTestBase
    {
        public void MyApplicationIsNotYetRegistered(string applicationName)
        {
            ApplicationsController.AppsStore.Clear();
        }

        public void MyApplicationHasAlreadyBeenRegistered(string applicationName)
        {
            ApplicationsController.AppsStore.Add(Guid.NewGuid(),
                new ApplicationForm
                {
                    Name = applicationName
                });
        }

        public void IRegisterMyApplicationViaPost(Uri uri, string applicationName)
        {
            Response = Server.HttpClient.PostAsJsonAsync(uri,
                new
                {
                    name = applicationName
                })
                .Result;
        }

        private HttpResponseMessage Response { get; set; }

        public void TheResponseStatusCodeShouldBe(HttpStatusCode statusCode)
        {
            Response.StatusCode.Should()
                .Be(statusCode);
        }

        public void TheResponseContentShouldBe<T>(T content)
        {
            Response.Content.ReadAsJsonAsync<T>()
                .Result.ShouldBeEquivalentTo(content);
        }

        public void TheResponseShouldHaveHeader<T>(Func<HttpResponseHeaders, T> header, T value)
        {
            header(Response.Headers)
                .Should()
                .Be(value);
        }

        [Test]
        public void ApplicationHasAlreadyBeenAdded()
        {
            this.Given(_ => _.MyApplicationHasAlreadyBeenRegistered("MyApplication"))
                .When(_ => _.IRegisterMyApplicationViaPost(new Uri("/applications", UriKind.Relative), "MyApplication"))
                .Then(_ => _.TheResponseStatusCodeShouldBe(HttpStatusCode.Conflict))
                .And(
                    _ =>
                        _.TheResponseShouldHaveHeader(h => h.Location,
                            new Uri("/applications/MyApplication", UriKind.Relative)))
                .And(_ => _.TheResponseContentShouldBe(new
                {
                    ErrorDescription = string.Format("An application named '{0}' already exists.", "MyApplication")
                }))
                .BDDfy();
        }

        [Test]
        public void NewApplication()
        {
            this.Given(_ => _.MyApplicationIsNotYetRegistered("MyApplication"))
                .When(_ => _.IRegisterMyApplicationViaPost(new Uri("/applications", UriKind.Relative), "MyApplication"))
                .Then(_ => _.TheResponseStatusCodeShouldBe(HttpStatusCode.Created))
                .And(
                    _ =>
                        _.TheResponseShouldHaveHeader(h => h.Location,
                            new Uri("/applications/MyApplication", UriKind.Relative)))
                .BDDfy();
        }
    }
}