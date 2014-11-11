using Autofac;

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
            _webApiConfiguration.RegisterWebApiComponents(builder);
        }
    }
}