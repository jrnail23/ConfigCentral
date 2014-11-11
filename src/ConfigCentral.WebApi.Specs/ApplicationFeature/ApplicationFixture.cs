using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Autofac;
using ConfigCentral.DataAccess.NHibernate;
using ConfigCentral.DomainModel;
using ConfigCentral.Infrastructure;
using FluentAssertions;
using NHibernate;

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
            using (var scope = _rootContainerScope.BeginLifetimeScope("AutofacWebRequest"))
            using (var uow = scope.Resolve<NHibernateUnitOfWork>())
            {
                uow.Session.Delete("from Application");
                uow.Commit();
            }
        }

        public void MyApplicationHasAlreadyBeenRegistered(string applicationName)
        {
            using (var scope = _rootContainerScope.BeginLifetimeScope("AutofacWebRequest"))
            using (var uow = scope.Resolve<IUnitOfWork>())
            {
                var repository = scope.Resolve<IApplicationRepository>();
                repository.Add(new Application(Guid.NewGuid(), applicationName));
                uow.Commit();
            }
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
            Response = _client.GetAsync(url)
                .Result;
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