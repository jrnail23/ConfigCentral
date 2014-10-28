using System.Web.Http;
using Owin;

namespace ConfigCentral.WebApi
{
    public class WebPipeline
    {
        public void Configuration(IAppBuilder application)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            application.UseWebApi(config);
        }
    }
}