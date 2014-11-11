using Autofac;
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

            return builder.Build();
        }
    }
}