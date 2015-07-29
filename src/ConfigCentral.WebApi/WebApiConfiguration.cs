using System;
using System.Web.Http;

namespace ConfigCentral.WebApi
{
    public class WebApiConfiguration
    {
        private readonly HttpConfiguration _httpConfiguration;

        public WebApiConfiguration(HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration;
        }

        public void Register(Action<HttpConfiguration> configure)
        {
            _httpConfiguration.MapHttpAttributeRoutes();
            _httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            configure(_httpConfiguration);
        }
    }
}