using Autofac;
using Autofac.Integration.WebApi;
using ConfigCentral.WebApi.Resources;

namespace ConfigCentral.WebApi
{
    public class WebApiModule : Module
    {
        private readonly WebApiConfiguration _webApiConfiguration;

        public WebApiModule(WebApiConfiguration webApiConfiguration)
        {
            _webApiConfiguration = webApiConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof (HomeController).Assembly);

            _webApiConfiguration.Register(configuration =>
            {
                builder.RegisterWebApiFilterProvider(configuration);
                builder.RegisterHttpRequestMessage(configuration);
            });
        }
    }
}