using System.Web.Http;
using Autofac.Integration.WebApi;
using Topshelf;

namespace ConfigCentral.WebApi.TopShelfHost
{
    internal class Program
    {
        private static int Main()
        {
            var exitCode = HostFactory.Run(host =>
            {
                var webApiConfiguration = new WebApiConfiguration(new HttpConfiguration());
                var rootContainer = new CompositionRoot().Compose(webApiConfiguration);
                webApiConfiguration.Register(
                    config =>
                        config.DependencyResolver =
                            new AutofacWebApiDependencyResolver(rootContainer));

                host.Service<ConfigCentralApplication>(
                    service => service.ConstructUsing(() => new ConfigCentralApplication())
                        .WhenStarted(svc => svc.Start(new OwinPipeline(rootContainer, webApiConfiguration)))
                        .WhenStopped(svc => svc.Stop()));
                host.SetDescription(
                    "An application to manage configuration info across applications and deployment environments.");
                host.SetDisplayName("Config Central");
                host.SetServiceName("ConfigCentral");
                host.RunAsNetworkService();
            });
            return (int) exitCode;
        }
    }
}