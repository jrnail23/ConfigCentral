using System.Web.Http;
using Autofac;
using ConfigCentral.Infrastructure;
using ConfigCentral.Infrastructure.DataAccess;
using ConfigCentral.Mediator;
using ConfigCentral.UseCases;

namespace ConfigCentral.WebApi
{
    public class CompositionRoot
    {
        public IContainer Compose(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new NHibernateModule());
            builder.RegisterModule(new WebApiModule(httpConfiguration));
            builder.RegisterModule(
                new ApplicationBusPlumbingModule().RegisterHandlerTypesIn(
                    typeof (FindApplicationByName).Assembly));
            return builder.Build();
        }
    }
}