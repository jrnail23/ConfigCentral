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
                var httpConfig = new HttpConfiguration();
                var rootContainer = new CompositionRoot().Compose(httpConfig);
                httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(rootContainer);

                host.Service<ConfigCentralApplication>(
                    service =>
                        service.ConstructUsing(() => new ConfigCentralApplication())
                               .WhenStarted(svc => svc.Start(new Startup(rootContainer, httpConfig)))
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