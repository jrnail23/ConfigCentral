using System.Data.SqlServerCe;
using ConfigCentral.DomainModel;
using NHibernate;
using NHibernate.Exceptions;

namespace ConfigCentral.Infrastructure.DataAccess
{
    public class SqlServerCe40ExceptionTranslatingDecorator : NHibernateUnitOfWork
    {
        private readonly NHibernateUnitOfWork _inner;

        public SqlServerCe40ExceptionTranslatingDecorator(NHibernateUnitOfWork inner)
        {
            _inner = inner;
        }

        public override ISession Session => _inner.Session;

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
                // TODO introduce logging
                var sqlCeException = e.InnerException as SqlCeException;

                if (sqlCeException != null &&
                    sqlCeException.NativeError == SqlCeNativeErrors.UniqueIndexViolation)
                    throw new DuplicateObjectException("object already exists");
                throw;
            }
        }

        public override void Rollback()
        {
            _inner.Rollback();
        }
    }
}