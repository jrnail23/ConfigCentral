using System.Configuration;
using Autofac;
using ConfigCentral.DomainModel;
using ConfigCentral.Infrastructure;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace ConfigCentral.WebApi
{
    public class NHibernateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
        }
    }
}