using System;
using System.IO;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ConfigCentral.Infrastructure;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace ConfigCentral.WebApi.Specs
{
    public abstract class ApiAcceptanceTestBase
    {
        protected TestServer Server;
        protected IContainer RootContainer { get; private set; }

        [SetUp]
        public void SetUpTestServer()
        {
            var webApiConfig = new WebApiConfiguration(new HttpConfiguration());
            RootContainer = new CompositionRoot().Compose(webApiConfig);
            webApiConfig.Register(
                config => config.DependencyResolver = new AutofacWebApiDependencyResolver(RootContainer));

            Server =
                TestServer.Create(app => new OwinPipeline(RootContainer, webApiConfig).Configuration(app));
            SetUpTestSpecificDatabase();
        }

        [TearDown]
        public void TearDownTestServer()
        {
            Server.Dispose();
            Server = null;
        }

        private void SetUpTestSpecificDatabase()
        {
            var dbTemplateFilePath = @"App_Data\ConfigCentral.sdf";
            var testSpecificDbFilePath = $@"App_Data\ConfigCentral_{Guid.NewGuid()}.sdf";
            File.Copy(dbTemplateFilePath, testSpecificDbFilePath);

            var connectionString = $"Data Source={testSpecificDbFilePath};";
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new NHibernateConfiguration(connectionString))
                .SingleInstance();
            builder.Update(RootContainer);
        }
    }
}