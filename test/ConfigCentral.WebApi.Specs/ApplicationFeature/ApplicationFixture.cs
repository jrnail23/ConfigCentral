using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Autofac;
using ConfigCentral.DomainModel;
using ConfigCentral.Infrastructure.DataAccess;
using FluentAssertions;

namespace ConfigCentral.WebApi.Specs.ApplicationFeature
{
    public class ApplicationFixture
    {
        private readonly HttpClient _client;
        private readonly ILifetimeScope _rootContainerScope;

        public ApplicationFixture(HttpClient client, ILifetimeScope rootContainerScope)
        {
            _client = client;
            _rootContainerScope = rootContainerScope;
        }

        public HttpResponseMessage Response { get; set; }

        public void MyApplicationIsNotYetRegistered(string applicationName)
        {
            using (var scope = _rootContainerScope.BeginLifetimeScope())
            using (var uow = scope.Resolve<NHibernateUnitOfWork>())
            {
                uow.Session.Delete("from Application");
                uow.Commit();
            }
        }

        public void MyApplicationHasAlreadyBeenRegistered(string applicationName)
        {
            using (var scope = _rootContainerScope.BeginLifetimeScope())
            using (var uow = scope.Resolve<IUnitOfWork>())
            {
                var repository = scope.Resolve<IApplicationRepository>();
                repository.Add(new Application(applicationName));
                uow.Commit();
            }
        }

        public void IRegisterMyApplicationViaPost(Uri uri, string applicationName)
        {
            Response = _client.PostAsJsonAsync(uri, new {name = applicationName}).Result;
        }

        public void INavigateToTheResponseLocation()
        {
            var url = Response.Headers.Location;
            Response = _client.GetAsync(url).Result;
        }

        public void TheResponseShouldNotBeAnError()
        {
            var statusCode = Response.StatusCode;

            Response.IsSuccessStatusCode.Should()
                    .BeTrue(
                        "because an error response was not expected.  However, the actual response was HTTP {0}({1}), with content '{2}'",
                        (int) statusCode, statusCode, Response.Content.ReadAsStringAsync().Result);
        }

        public void TheResponseStatusCodeShouldBe(HttpStatusCode statusCode)
        {
            var actual = Response.StatusCode;
            Response.StatusCode.Should()
                    .Be(statusCode,
                        "because the specified expected status code was HTTP {0}({1}).  However, the actual response was HTTP {2}({3}), with content:'{4}'",
                        (int) statusCode, statusCode, (int) actual, actual,
                        Response.Content.ReadAsStringAsync().Result);
        }

        public void TheResponseContentShouldBe<T>(T content)
        {
            Response.Content.ReadAsJsonAsync<T>().Result.ShouldBeEquivalentTo(content);
        }

        public void TheResponseShouldHaveHeader<T>(Func<HttpResponseHeaders, T> header, T value)
        {
            header(Response.Headers).Should().Be(value);
        }

        public void IGet(Uri uri)
        {
            Response = _client.GetAsync(uri).Result;
        }
    }
}