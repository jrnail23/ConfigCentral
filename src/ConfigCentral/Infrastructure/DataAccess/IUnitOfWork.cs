using System;

namespace ConfigCentral.Infrastructure.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}