using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace ConfigCentral.WebApi
{
    public class WebApiModule : Module
    {
        private readonly HttpConfiguration _httpConfiguration;

        public WebApiModule(HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(ThisAssembly);
            //builder.RegisterWebApiFilterProvider(_httpConfiguration);
            builder.RegisterHttpRequestMessage(_httpConfiguration);
        }
    }
}