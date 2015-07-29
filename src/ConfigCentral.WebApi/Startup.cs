using System.Web.Http;
using Autofac;
using Owin;

namespace ConfigCentral.WebApi
{
    public class Startup
    {
        private readonly IContainer _rootLifetimeScope;
        private readonly HttpConfiguration _httpConfiguration;

        public Startup(IContainer rootLifetimeScope, HttpConfiguration httpConfiguration)
        {
            _rootLifetimeScope = rootLifetimeScope;
            _httpConfiguration = httpConfiguration;
        }

        public void Configuration(IAppBuilder application)
        {
            _httpConfiguration.MapHttpAttributeRoutes();
            _httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            application.UseAutofacMiddleware(_rootLifetimeScope);
            application.UseAutofacWebApi(_httpConfiguration);
            application.UseWebApi(_httpConfiguration);
        }
    }
}