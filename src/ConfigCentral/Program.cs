using Topshelf;

namespace ConfigCentral
{
    internal class Program
    {
        private static int Main()
        {
            var exitCode = HostFactory.Run(host =>
            {
                host.Service<ConfigCentralApplication>(
                    service => service.ConstructUsing(() => new ConfigCentralApplication())
                        .WhenStarted(svc => svc.Start())
                        .WhenStopped(svc => svc.Stop()));
                host.SetDescription("An application to manage configuration info across applications and deployment environments.");
                host.SetDisplayName("Config Central");
                host.SetServiceName("ConfigCentral");
                host.RunAsNetworkService();
            });
            return (int)exitCode;
        }
    }
}
