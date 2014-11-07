using System.Web.Http;
using Autofac;
using Owin;

namespace ConfigCentral.WebApi
{
    public class WebPipeline
    {
        private readonly ILifetimeScope _rootLifetimeScope;

        public WebPipeline(ILifetimeScope rootLifetimeScope)
        {
            _rootLifetimeScope = rootLifetimeScope;
        }

        public void Configuration(IAppBuilder application)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            application.UseAutofacMiddleware(_rootLifetimeScope);
            application.UseAutofacWebApi(config);
            application.UseWebApi(config);
        }


    }
}