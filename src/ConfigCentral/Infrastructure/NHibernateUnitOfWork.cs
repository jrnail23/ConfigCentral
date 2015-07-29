using NHibernate;

namespace ConfigCentral.Infrastructure
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private ISession _session;
        protected NHibernateUnitOfWork() {}

        public NHibernateUnitOfWork(ISession session)
        {
            _session = session;
            _session.BeginTransaction();
        }

        public virtual ISession Session => _session;

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
            if (Session == null) return;
            if (Session.Transaction.IsActive && !Session.Transaction.WasCommitted &&
                !Session.Transaction.WasRolledBack)
            {
                Session.Transaction.Rollback();
            }
            Session.Close();
            Session.Dispose();
            _session = null;
        }
    }
}