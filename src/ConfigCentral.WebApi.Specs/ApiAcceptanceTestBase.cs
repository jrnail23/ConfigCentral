using System;
using System.IO;
using Autofac;
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
            var webApiConfig = new WebApiConfiguration();
            RootContainer = new CompositionRoot().Compose(webApiConfig);

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
            var testSpecificDbFilePath = string.Format(@"App_Data\ConfigCentral_{0}.sdf", Guid.NewGuid());
            File.Copy(dbTemplateFilePath, testSpecificDbFilePath);

            var connectionString = string.Format("Data Source={0};", testSpecificDbFilePath);
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new NHibernateConfiguration(connectionString))
                .SingleInstance();

            builder.Update(RootContainer);
        }
    }
}