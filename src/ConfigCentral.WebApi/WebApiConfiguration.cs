using System;
using System.Web.Http;
using System.Web.Http.Filters;
using Autofac;
using Autofac.Integration.WebApi;
using ConfigCentral.WebApi.Resources;
using Owin;

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

        public IAppBuilder RegisterWith(IAppBuilder app)
        {
            return app.UseAutofacWebApi(_httpConfig)
                .UseWebApi(_httpConfig);
        }

        public void RegisterWebApiComponents(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof (HomeController).Assembly);
            RegisterWebApiFilterProvider(builder);
        }

        private void RegisterWebApiFilterProvider(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            _httpConfig.Services.RemoveAll(typeof (IFilterProvider),
                provider => provider is ActionDescriptorFilterProvider);

            builder.Register(c => new AutofacWebApiFilterProvider(c.Resolve<ILifetimeScope>()))
                .As<IFilterProvider>()
                .SingleInstance();
        }
    }
}