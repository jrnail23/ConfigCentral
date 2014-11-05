using Autofac;
using Autofac.Integration.WebApi;
using ConfigCentral.DomainModel;
using ConfigCentral.Infrastructure;
using ConfigCentral.WebApi.Resources;

namespace ConfigCentral.WebApi
{
    public class CompositionRoot
    {
        public ILifetimeScope Compose()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(HomeController).Assembly);

            builder.RegisterType<InMemoryApplicationRepository>()
                .As<IApplicationRepository>();

            return builder.Build();
        }
    }
}