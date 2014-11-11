using System;
using System.Data.SqlServerCe;
using ConfigCentral.DomainModel;
using NHibernate;
using NHibernate.Exceptions;

namespace ConfigCentral.Infrastructure
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;

        protected NHibernateUnitOfWork()
        {
            
        }

        public NHibernateUnitOfWork(ISession session)
        {
            _session = session;
            _session.BeginTransaction();
        }

        public virtual ISession Session
        {
            get { return _session; }
        }

        public virtual void Commit()
        {
            try
            {
                Session.Transaction.Commit();
            }
            catch (HibernateException)
            {
                Rollback();
                throw;
            }
        }

        public virtual void Rollback()
        {
            Session.Transaction.Rollback();
        }

        public virtual void Dispose()
        {
            if (Session.Transaction.IsActive 
                && !Session.Transaction.WasCommitted 
                && !Session.Transaction.WasRolledBack)
            {
                Session.Transaction.Rollback();
            }

            Session.Dispose();
        }
    }

    public class SqlServerCe40ExceptionTranslatingDecorator : NHibernateUnitOfWork
    {
        private readonly NHibernateUnitOfWork _inner;

        public SqlServerCe40ExceptionTranslatingDecorator(NHibernateUnitOfWork inner)
        {
            _inner = inner;
        }

        public override void Dispose()
        {
            _inner.Dispose();
        }

        public override void Commit()
        {
            try
            {
                _inner.Commit();
            }
            catch (GenericADOException e)
            {
                Console.WriteLine(e);
                var sqlCeException = e.InnerException as SqlCeException;

                if (sqlCeException != null && sqlCeException.NativeError == SqlCeNativeErrors.UniqueIndexViolation)
                {
                    throw new DuplicateObjectException("object already exists");
                }
                throw;
            }
        }

        public override ISession Session
        {
            get { return _inner.Session; }
        }

        public override void Rollback()
        {
            _inner.Rollback();
        }
    }
}