using System.Collections.Generic;
using ConfigCentral.DomainModel;
using NHibernate;
using ObjectNotFoundException = ConfigCentral.DomainModel.ObjectNotFoundException;

namespace ConfigCentral.Infrastructure
{
    public class NHibernateApplicationRepository : IApplicationRepository
    {
        private readonly ISession _session;

        public NHibernateApplicationRepository(ISession session)
        {
            _session = session;
        }

        public Application FindByName(string name)
        {
            var app = _session.QueryOver<Application>()
                .Where(a => a.Name == name)
                .SingleOrDefault();
            if (app == null)
                throw new ObjectNotFoundException("Could not find an application named '" + name + "'.");

            return app;
        }

        public IEnumerable<Application> All()
        {
            return _session.QueryOver<Application>()
                .List();
        }

        public void Add(Application application)
        {
            _session.Save(application);
        }
    }
}