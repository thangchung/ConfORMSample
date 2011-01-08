using System;

namespace ConfORMSample.Core.Persistence
{
    public interface IDbContext
    {
        void CommitChanges();

        IDisposable BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}