using Autofac;
using Owin;

namespace ConfigCentral.WebApi
{
    public class OwinPipeline
    {
        private readonly IContainer _rootLifetimeScope;
        private readonly WebApiConfiguration _webApiConfiguration;

        public OwinPipeline(IContainer rootLifetimeScope, WebApiConfiguration webApiConfiguration)
        {
            _rootLifetimeScope = rootLifetimeScope;
            _webApiConfiguration = webApiConfiguration;
        }

        public void Configuration(IAppBuilder application)
        {
            application.UseAutofacMiddleware(_rootLifetimeScope);
            _webApiConfiguration.Register(httpConfig => application.UseAutofacWebApi(httpConfig)
                .UseWebApi(httpConfig));
        }
    }
}