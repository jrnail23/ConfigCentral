using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ConfigCentral.DomainModel;
using ConfigCentral.Infrastructure;
using FluentAssertions;

namespace ConfigCentral.WebApi.Specs.ApplicationFeature
{
    public class ApplicationFixture
    {
        private readonly HttpClient _client;

        public ApplicationFixture(HttpClient client)
        {
            _client = client;
        }

        public HttpResponseMessage Response { get; set; }

        public void MyApplicationIsNotYetRegistered(string applicationName)
        {
            InMemoryApplicationRepository.AppsDataStore.Clear();
        }

        public void MyApplicationHasAlreadyBeenRegistered(string applicationName)
        {
            InMemoryApplicationRepository.AppsDataStore.Add(new Application(applicationName));
        }

        public void IRegisterMyApplicationViaPost(Uri uri, string applicationName)
        {
            Response = _client.PostAsJsonAsync(uri,
                new
                {
                    name = applicationName
                })
                .Result;
        }

        public void INavigateToTheResponseLocation()
        {
            var url = Response.Headers.Location;
            Response = _client.GetAsync(url).Result;
        }

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

        public void IGet(Uri uri)
        {
            Response = _client.GetAsync(uri)
                .Result;
        }
    }
}