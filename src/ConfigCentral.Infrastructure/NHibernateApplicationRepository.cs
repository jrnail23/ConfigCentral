using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using ConfigCentral.DomainModel;
using NHibernate;
using NHibernate.Exceptions;
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
            // TODO: introduce unit of work
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(application);
                    tx.Commit();
                }
                catch (GenericADOException e)
                {
                    tx.Rollback();
                    var sqlCeException = e.InnerException as SqlCeException;

                    if (sqlCeException != null && sqlCeException.NativeError == SqlCeNativeErrors.UniqueIndexViolation)
                    {
                        throw new DuplicateObjectException(string.Format("An application named '{0}' already exists.",
                            application.Name));
                    }
                    throw;
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}