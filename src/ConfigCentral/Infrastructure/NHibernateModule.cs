using System.Configuration;
using Autofac;
using ConfigCentral.DomainModel;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace ConfigCentral.Infrastructure
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
                .Named<ISession>("default")
                .OnActivated(args => { args.Instance.FlushMode = FlushMode.Commit; })
                .InstancePerLifetimeScope();

            builder.Register(c => new NHibernateUnitOfWork(c.ResolveNamed<ISession>("default")))
                .Named<NHibernateUnitOfWork>("default")
                .InstancePerLifetimeScope();

            builder.RegisterDecorator<NHibernateUnitOfWork>(
                (c, inner) => new SqlServerCe40ExceptionTranslatingDecorator(inner),
                fromKey: "default")
                .As<NHibernateUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<NHibernateUnitOfWork>().Session)
                .As<ISession>()
                .ExternallyOwned()
                .InstancePerLifetimeScope();
        }
    }
}