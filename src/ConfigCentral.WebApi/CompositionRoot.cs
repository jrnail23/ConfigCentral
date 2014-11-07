using System.Configuration;
using Autofac;
using Autofac.Integration.WebApi;
using ConfigCentral.DomainModel;
using ConfigCentral.Infrastructure;
using ConfigCentral.WebApi.Resources;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace ConfigCentral.WebApi
{
    public class CompositionRoot
    {
        public IContainer Compose()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof (HomeController).Assembly);

            //InMemoryApplicationRepository
            builder.RegisterType<NHibernateApplicationRepository>()
                .As<IApplicationRepository>();

            builder.Register(
                c =>
                    new NHibernateConfiguration(ConfigurationManager.ConnectionStrings["ConfigCentral"].ConnectionString))
                .SingleInstance();

            builder.Register(c => c.Resolve<NHibernateConfiguration>()
                .Configure())
                .SingleInstance();

            builder.Register(c => c.Resolve<Configuration>()
                .BuildSessionFactory())
                .As<ISessionFactory>()
                .SingleInstance();

            builder.Register(c => c.Resolve<ISessionFactory>()
                .OpenSession())
                .As<ISession>()
                .OnActivated(args => args.Instance.FlushMode = FlushMode.Commit)
                .InstancePerRequest();

            return builder.Build();
        }
    }
}