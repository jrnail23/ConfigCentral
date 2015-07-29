using Autofac;
using ConfigCentral.Infrastructure;
using ConfigCentral.Mediator;
using ConfigCentral.UseCases;

namespace ConfigCentral.WebApi
{
    public class CompositionRoot
    {
        public IContainer Compose(WebApiConfiguration webApiConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new NHibernateModule());
            builder.RegisterModule(new WebApiModule(webApiConfiguration));
            builder.RegisterModule(
                new ApplicationBusPlumbingModule().RegisterHandlerTypesIn(typeof (FindApplicationByName).Assembly));
            return builder.Build();
        }
    }
}