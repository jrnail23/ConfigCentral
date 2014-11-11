using System;
using System.Web.Http;

namespace ConfigCentral.WebApi
{
    public class WebApiConfiguration
    {
        private readonly HttpConfiguration _httpConfig;

        public WebApiConfiguration()
        {
            _httpConfig = new HttpConfiguration();
            ConfigureWebApi(_httpConfig);
        }

        private void ConfigureWebApi(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }

        public void Register(Action<HttpConfiguration> configure)
        {
            configure(_httpConfig);
        }
    }
}