using Autofac;
using ConfigCentral.Application;
using ConfigCentral.ApplicationBus;
using ConfigCentral.DataAccess.NHibernate;

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